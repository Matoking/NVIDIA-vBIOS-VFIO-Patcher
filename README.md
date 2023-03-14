# NVIDIA vBIOS VFIO Patcher

**This tool is known to be compatible with RTX 3080 Ti, GTX 1060, GTX 1050 Ti, GT 710 NVIDIA GPUs.**
(Should also work with: RTX 20XX - RTX 30XX, but it's not tested yet, please create an issue if you have tested it.)

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

# Proxmox PVE 7 Example
Copy your <PATCHED_ROM> to /usr/share/kvm/<PATCHED_ROM>

```
args: -cpu 'host,+kvm_pv_unhalt,+kvm_pv_eoi,hv_vendor_id=proxmox,hv_spinlocks=0x1fff,hv_vapic,hv_time,hv_reset,hv_vpindex,hv_runtime,hv_relaxed,hv_synic,hv_stimer,hv_tlbflush,hv_ipi,kvm=off'
cpu: host,hidden=1,flags=+pcid
machine: pc-q35-3.1
hostpci0: 01:00,pcie=1,romfile=GTX1650TiPatched.rom,x-vga=1
```

You have to set the following kernel lines: *(set the ZFS parameter only if you have a ZFS root drive)*

*Grub:* /etc/default/grub
```
GRUB_CMDLINE_LINUX_DEFAULT="quiet amd_iommu=on iommu=pt video=vesafb:off video=efifb:off"
GRUB_CMDLINE_LINUX="root=ZFS=rpool/ROOT/pve-1 boot=zfs" 
```
Update bootloader:
```
update-grub
```

*UEFI:* /etc/kernel/cmdline
```
quiet root=ZFS=rpool/ROOT/pve-1 boot=zfs amd_iommu=on iommu=pt video=vesafb:off video=efifb:off
```
Update bootloader:
```
proxmox-boot-tool refresh
```
# Proxmox PVE 6 Example
```
args: -cpu 'host,+kvm_pv_unhalt,+kvm_pv_eoi,hv_vendor_id=proxmox,hv_spinlocks=0x1fff,hv_vapic,hv_time,hv_reset,hv_vpindex,hv_runtime,hv_relaxed,hv_synic,hv_stimer,hv_tlbflush,hv_ipi,kvm=off'
cpu: host,hidden=1,flags=+pcid
machine: pc-q35-3.1
hostpci0: 01:00,pcie=1,romfile=GTX1650TiPatched.rom,x-vga=1
```
