# WindowsDisplayAPI

> [!NOTE]
> This is a modified fork of the original [WindowsDisplayAPI](https://github.com/falahati/WindowsDisplayAPI) library maintained for the [Lenovo Legion Toolkit](https://github.com/LenovoLegionToolkit-Team/LenovoLegionToolkit) project.

[![NuGet Version](https://img.shields.io/nuget/v/LLT.WindowsDisplayAPI.svg?style=flat-square)](https://www.nuget.org/packages/LLT.WindowsDisplayAPI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LLT.WindowsDisplayAPI.svg?style=flat-square)](https://www.nuget.org/packages/LLT.WindowsDisplayAPI)
[![License](https://img.shields.io/github/license/LenovoLegionToolkit-Team/WindowsDisplayAPI.svg?style=flat-square)](https://github.com/LenovoLegionToolkit-Team/WindowsDisplayAPI/blob/master/LICENSE)
[![Commit Activity](https://img.shields.io/github/commit-activity/y/LenovoLegionToolkit-Team/WindowsDisplayAPI.svg?style=flat-square)](https://github.com/LenovoLegionToolkit-Team/WindowsDisplayAPI/commits/master)
[![Issues](https://img.shields.io/github/issues/LenovoLegionToolkit-Team/WindowsDisplayAPI.svg?style=flat-square)](https://github.com/LenovoLegionToolkit-Team/WindowsDisplayAPI/issues)

**WindowsDisplayAPI** is a lightweight .NET wrapper for the Windows Display and Connecting and Configuring Displays (CCD) APIs. It enables developers to query, configure, and manage display topologies, resolutions, refresh rates, and scaling settings programmatically.

## Installation

The library is available on NuGet. You can install it using the NuGet Package Manager Console:

```shell
dotnet add package LLT.WindowsDisplayAPI
```

For more details, visit the [LLT.WindowsDisplayAPI NuGet Page](https://www.nuget.org/packages/LLT.WindowsDisplayAPI).

## Features & Windows API Support

| Feature / Interface | Subsystem / Flag | Status | Description |
|---|---|---|---|
| **Display Topologies & Modes** | CCD (`Clone`, `Extend`, `Internal`, `External`) | Supported | Full topology querying, path switching, and validation. |
| **Resolution & Refresh Rates** | CCD (Standard) | Supported | Custom resolutions, progressive/interlaced refresh rate rationales. |
| **Dynamic Refresh Rate (DRR)** | CCD (`BoostRefreshRate`, `VirtualRefreshRateAware`) | Supported | Full support for Windows 11 Dynamic Refresh Rate boost paths. |
| **DPI Scaling** | CCD (`GetSourceDPIScale`, `SetSourceDPIScale`) | Supported | Per-source DPI scale percentage reading and modification. |
| **Virtual Resolution** | CCD (`Get/SetSupportVirtualResolution`) | Supported | Virtual mode and virtual resolution configuration. |
| **HDR / Advanced Color Info** | CCD (`GET_ADVANCED_COLOR_INFO` = 9) | In Progress (In LLT) | Query HDR and Wide Color Gamut (WCG) capability and active state. |
| **HDR State Control** | CCD (`SET_ADVANCED_COLOR_STATE` = 10/14) | In Progress (In LLT) | Programmatically toggle HDR on/off per display. |
| **SDR White Level in HDR** | CCD (`GET_SDR_WHITE_LEVEL` = 11) | Planned | Read SDR content brightness slider value (nits) in HDR mode. |
| **Monitor Specialization** | CCD (`GET/SET_MONITOR_SPECIALIZATION` = 12/13) | Planned | Check or configure specialized displays (HMD / VR / Medical panels). |
| **Advanced Color Primaries** | CCD (`GET_ADVANCED_COLOR_INFO_2` = 15) | Planned | Retrieve color gamut chromaticity coordinates and peak luminance. |
| **DDC/CI Hardware Controls** | DXVA2 (`dxva2.dll`) | Planned | External monitor hardware brightness, contrast, and VCP inputs. |
| **DXGI Hardware Color Metrics** | DXGI (`DXGI_OUTPUT_DESC1`) | Planned | Real panel peak luminance (nits), bit depth, and color space gamut. |
| **Windows Color Management** | WCS / ICM (`mscms.dll`) | Planned | Query and assign ICC/ICM color profiles per display. |

## Sponsorship & Funding

If you find this library useful, please consider supporting the project and its authors:

### Project Maintainer
Support the active maintainer of this fork:
* [**Sponsor Metanome on GitHub**](https://github.com/sponsors/Metanome)

### Original Author
Support the original creator and author of the library, Soroush Falahati:
* [**CoinPayments (Crypto)**](https://www.coinpayments.net/index.php?cmd=_donate&reset=1&merchant=820707aded07845511b841f9c4c335cd&item_name=Donate&currency=USD&amountf=20.00000000&allow_amount=1&want_shipping=0&allow_extra=1)
* [**ZarinPal**](https://zarinp.al/@falahati)
* [**PayPal (USD)**](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=ramin.graphix@gmail.com&lc=US&item_name=Donate&no_note=0&cn=&curency_code=USD&bn=PP-DonationsBF:btn_donateCC_LG.gif:NonHosted)

*Your contributions, bug reports, and pull requests are always welcome.*

## Related Projects

* [**NvAPIWrapper**](https://github.com/falahati/NvAPIWrapper/): A .NET wrapper for the NVIDIA public API, allowing detailed display and GPU management on NVIDIA systems.
* [**EDIDParser**](https://github.com/falahati/EDIDParser/): A helper library designed to parse and extract hardware information from raw EDID binary structures.
* [**HeliosDisplayManagement**](https://github.com/falahati/HeliosDisplayManagement/): An open-source display profile manager for Windows supporting NVIDIA Surround configurations.

## License

Copyright (C) 2017-2020 Soroush Falahati.

This project is licensed under the **GNU Lesser General Public License (LGPL-3.0)**, which allows for use in closed-source or commercial projects.

Per the license terms, any modifications to the core library code must be made public. Refer to the [LICENSE](LICENSE) file for the full text of the license.