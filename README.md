**One Identity open source projects are supported through [One Identity GitHub issues](https://github.com/OneIdentity/IdentityManager.PoSh-Connector-Generator/issues) and the [One Identity Community](https://www.oneidentity.com/community/). This includes all scripts, plugins, SDKs, modules, code snippets or other solutions. For assistance with any One Identity GitHub project, please raise a new Issue on the [One Identity GitHub project](https://github.com/OneIdentity/IdentityManager.PoSh-Connector-Generator/issues) page. You may also visit the [One Identity Community](https://www.oneidentity.com/community/) to ask questions. Requests for assistance made through official One Identity Support will be referred back to GitHub and the One Identity Community forums where those requests can benefit all users.**

<!-- IdentityManager.PoSh-Connector-Generator -->
# IdentityManager.PoSh-Connector-Generator
Identity Manager provides a rich set of [out-of-the-box integrations](https://www.oneidentity.com/one-identity-manager-integration). This includes many generic connectors and cloud application connectors via [Starling Connect](https://www.oneidentity.com/products/starling-connect/).

To connect non-generic unsupported systems Identity Manager also provides you with the option of building your own custom connector using Windows PowerShell.

[Creating a PowerShell Synchronization Project](https://support.oneidentity.com/de-de/technical-documents/identity-manager/9.1.1/windows-powershell-connector-user-guide/2#TOPIC-1994514) is a multi-step process.

The first and crucial step in that process is creating a Connector Definition File (**CDF**), written in XML, which describes the structure of the target system and the Windows PowerShell cmdlets to use.

You can find an example of a definition file on the One Identity Manager installation medium in:
/Modules/TSB/dvd/AddOn/SDK/ADSample.xml

This solution accelerator simplifies the step of creating the CDF by providing a UI to graphically configure the interface which will be exposed to the Identity Manager Synchronization Engine.

The solution accelerator generates the CDF and also an accompanying **PowerShell Module** or **C# Class**. The generated code leverages the Façade Pattern with one auto-created method per CRUD operation. By using this approach developers can cleanly separate the interface definition and connector code.

The actual connector code can then be developed seperately in **PowerShell** or **C#**, tested and then plugged into the generated structure.


<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li><a href="#supported-versions">Supported Versions</a></li>
    <li><a href="#requirements">Requirements</a></li>
    <li><a href="#limitations">Limitations</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- Supported Versions -->
## Supported Versions

This solution accelerator works with all supported versions of Identity Manager.

[:top:](#table-of-contents)

<!-- Requirements -->
## Requirements

* [.NET Desktop Runtime 5.0.17]()

<!-- Limitations -->
## Limitations

This solution accelerator does not support reading and modifying existing Connector Definition Files.

<!-- Contributing -->
## Contributing

[:top:](#table-of-contents)

Once we have uploaded the source code we look forwrad to accepting community contributions.

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **highly appreciated**.

1. Fork [this project](https://github.com/OneIdentity/IdentityManager.PoSh)
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

[:top:](#table-of-contents)

<!-- LICENSE -->
## License

Distributed under the One Identity - Open Source License. See [LICENSE](LICENSE.md) for more information.

[:top:](#table-of-contents)
