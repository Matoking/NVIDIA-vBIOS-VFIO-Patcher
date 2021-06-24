# NVIDIA vBIOS VFIO Patcher GUI

This is a fork of [this script.](https://github.com/Matoking/NVIDIA-vBIOS-VFIO-Patcher)

Simple tool for patching Nvidia GPU ROMs for VFIO/Passthrough, most of the credit goes to Matoking, I only added a .net frontend to it.

Supports 4XX, 5XX, 6XX, 7XX, 9XX, 1XXX, 2XXX, and 3XXX. Patched ROM will be in the same directory as the original ROM, and renamed to <original_name>patch.rom

The patching process requires a full copy of the clean vBIOS. You can either extract it from the graphics card using [nvflash](https://www.techpowerup.com/download/nvidia-nvflash/) or [GPU-Z](https://www.techpowerup.com/gpuz/) under Windows (recommended), or download one for your specific GPU model from [TechPowerUp](https://www.techpowerup.com/vgabios/).

# DISCLAIMER

**Use this script at your own discretion. This script has NOT been tested extensively, and has only been tested with a few GPUs.**

**The script performs only a few rudimentary sanity checks, but no guarantees are made of the validity of the patched ROM!**

# Usage

The script should work with both Python 2 and 3, and requires .net 4.5.

