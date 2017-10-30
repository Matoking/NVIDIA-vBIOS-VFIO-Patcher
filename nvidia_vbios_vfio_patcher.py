#!/usr/bin/env python

from __future__ import print_function

import sys
import binascii
import argparse
import re


# raw_input doesn't exist in Python 3
try:
    raw_input
except NameError:
    raw_input = input


PROMPT_TEXT = "I agree to be careful"

WARNING_TEXT = """
USE THIS SOFTWARE AT YOUR OWN DISCRETION. THIS SOFTWARE HAS *NOT* BEEN
EXTENSIVELY TESTED AND MAY NOT WORK WITH YOUR GRAPHICS CARD.

If you want to save the created vBIOS file, type the following phrase
EXACTLY as it is written below:

%s
""" % PROMPT_TEXT


class CheckException(Exception):
    pass


class VBIOSROM(object):
    def __init__(self, f):
        """
        Load a VBIOS and convert it into a hex-ascii format
        for easier editing
        """
        content = f.read()
        self.offsets = {
            "header": None,
            "footer": None
        }
        self.content = binascii.hexlify(content)

    def detect_offsets(self):
        """
        Search the ROM for known sections of data and raise an AssertionError
        if any of the checks fails
        """
        # Search for the header that starts the file
        # Examples of this header:
        #
        # U.y.K7400.L.w.VIDEO
        # U.x.K7400.L.w.VIDEO
        #
        HEADER_REGEX = (
            b'55aa(([a-z]|[0-9]){2})(eb)(([a-z]|[0-9]){20})(564944454f)'
        )
        result = re.compile(HEADER_REGEX).search(self.content)

        if not result or len(result.groups()) != 6:
            raise CheckException("Couldn't find the ROM header!")

        self.offsets["header"] = result.start(0)

        # Search for the footer, which are shortly followed by
        # 'NPDS' and 'NPDE' strings. 'NPDS' and 'NPDE' markers are separated by
        # 28 ASCII characters
        FOOTER_REGEX = (
            b'564e(([a-z]|[0-9]){348})(4e504453)(([a-z]|[0-9]){56})(4e504445)'
        )
        result = re.compile(FOOTER_REGEX).search(self.content)
        if not result or len(result.groups()) != 6:
            raise CheckException("Couldn't find the ROM footer!")

        self.offsets["footer"] = result.start(0)

    def run_sanity_tests(self, ignore_check=False):
        """
        Run a few sanity tests on the ROM to be a little more sure we are
        working with a valid ROM
        """
        try:
            # There should be one 'NPDS' marker and three 'NPDE' markers
            # before the footer we've already found
            #
            # The 'NPDS' marker should be followed by two 'NPDE' markers
            npds_count = self.content.count(
                b"4e504453", self.offsets["header"], self.offsets["footer"])
            if npds_count != 1:
                raise CheckException(
                    "Expected only one 'NPDS' marker between header and "
                    "footer, found %d" % npds_count)

            npde_count = self.content.count(
                b"4e504445", self.offsets["header"], self.offsets["footer"])
            if npde_count != 3:
                raise CheckException(
                    "Expected only three 'NPDE' markers between header and "
                    "footer, found %d" % npde_count)

            npde_after_npds_count = self.content.count(
                b"4e504445", self.content.find(b"4e504453"),
                self.offsets["footer"])

            if npde_after_npds_count != 2:
                raise CheckException(
                    "Expected two 'NPDE' markers after the 'NPDS' marker")
        except CheckException as e:
            if ignore_check:
                print("Encountered error during sanity check: %s" % str(e))
                print("Ignoring...")
                return
            else:
                raise

        print("No problems found.")

    def get_spliced_rom(self):
        """
        Convert the internal hex-ascii representation of the ROM
        into binary data for saving
        """
        start = self.offsets["header"]
        end = self.offsets["footer"]
        spliced = self.content[start:end]

        return binascii.unhexlify(spliced)


def main():
    parser = argparse.ArgumentParser(
        description=(
            "Convert a full NVIDIA Pascal vBIOS ROM into a form compatible "
            "for PCI passthrough."
        )
    )
    parser.add_argument(
        "-i", type=str, required=True,
        help="The full ROM to read")
    parser.add_argument(
        "-o", type=str, required=True,
        help="Path for saving the newly generated ROM")
    parser.add_argument(
        "--ignore-sanity-check", default=False, action="store_true",
        help="Don't halt the script if any of the sanity checks fails"
    )
    parser.add_argument(
        "--skip-the-very-important-warning",
        default=False, action="store_true",
        help=(
            "Skip the very important warning and save the ROM without asking "
            "for any input."
        )
    )

    args = parser.parse_args()

    print("Opening the ROM file...")

    with open(args.i, "rb") as f:
        rom = VBIOSROM(f)

    print("Scanning for ROM offsets...")
    rom.detect_offsets()
    print("Offsets found!")

    print("Running sanity checks...")
    rom.run_sanity_tests(args.ignore_sanity_check)

    spliced_rom = rom.get_spliced_rom()

    if not args.skip_the_very_important_warning:
        print(WARNING_TEXT)
        print("Type here: ", end="")
        answer = raw_input()

        if answer != PROMPT_TEXT:
            print("Wrong answer, halting...")
            sys.exit(1)

    print("Writing the edited ROM...")

    with open(args.o, "wb") as f:
        f.write(spliced_rom)

    print("Done!")


if __name__ == "__main__":
    main()
