# NVIDIA vBIOS VFIO Patcher

**This tool is known to be compatible only with the Pascal series (1xxx) of NVIDIA GPUs.**

nvidia_vbios_vfio_patcher.py is a script that creates a patched/spliced copy of a NVIDIA vBIOS that allows PCI passthrough when using libvirt. This copy of the vBIOS can then be passed to libvirt, allowing the NVIDIA GPU to be used in the guest VM. This can be done by adding the following line to the VM domain XML file.

```
   <hostdev>
     ...
     <rom file='/path/to/your/patched/gpu/bios.bin'/>
     ...
   </hostdev>
```

This script may be useful if you are using one of the Pascal (1xxx series) series of NVIDIA GPUs and you are having passing the GPU to the guest VM. In this case, the vBIOS of the system's primary GPU is tainted when booting the host OS, making GPU passthrough impossible unless a clean copy of the vBIOS is used.

The patching process requires a full copy of the clean vBIOS. You can either extract it from the graphics card using [nvflash](https://www.techpowerup.com/download/nvidia-nvflash/) or [GPU-Z](https://www.techpowerup.com/gpuz/) under Windows (recommended), or download one for your specific GPU model from [TechPowerUp](https://www.techpowerup.com/vgabios/).

# DISCLAIMER

**Use this script at your own discretion. This script has NOT been tested extensively, and has only been tested with a few GPUs belonging to he Pascal series of NVIDIA GPUs.**

**The script performs only a few rudimentary sanity checks, but no guarantees are made of the validity of the patched ROM!**

# Usage

The script should work with both Python 2 and 3.

To create a patched version of the BIOS, run the script with the following parameters.

```
python nvidia_vbios_vfio_patcher.py -i <ORIGINAL_ROM> -o <PATCHED_ROM>
```

A patched version of <ORIGINAL_ROM> will be written to <PATCHED_ROM>.
