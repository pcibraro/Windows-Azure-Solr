Solr/Lucene on Azure
===
In this project we showcase how to configure and host Solr/Lucene in Windows Azure using multi-instance replication for index-serving and single-instance for index generation with a persistent index mounted in Azure storage. Typical scenarios we address with this sample are commercial and publisher sites that need to scale the traffic with increasing query volume and need to index maximum 16 TB of data and require couple of index updates per day.

As part of this install the following Microsoft or third party software will be installed on your local machine as following: 

- Solr/Lucene which is owned by The Apache Software Foundation., will be downloaded from http://www.apache.org/dyn/closer.cgi/lucene/solr/.The license agreement to Apache License, Version 2.0 may be included with the software.  You are responsible for and must separately locate, read and accept these license terms.

- Microsoft Windows Azure SDK for .Net and NodeJS which is owned by Microsoft , will be downloaded from http://www.microsoft.com/windowsazure/sdk/.

You are responsible for and must locate and read the license terms for each of the software above. 

## Prerequisites for installer

1. Windows machine: Windows 7 (64 bit) or Windows Server 2008 R2 (64 bit)

2. IIS including the web roles ASP.Net, Tracing, logging & CGI Services needs to be enabled.
    - http://learn.iis.net/page.aspx/29/installing-iis-7-and-above-on-windows-server-2008-or-windows-server-2008-r2/ 
  
3. .Net Framework 4.0 Full version
   
4. Download JRE for Windows 64-bit which is owned by Sun Microsystems, Inc., from http://www.java.com/en/download/manual.jsp  . You are responsible for reading and accepting the license terms.

5. Note if you start with a clean machine:  To download your publishSettings file, the enhanced security configuration of IE needs to be disabled. Go to Server Manager -> configure IE ESC -> disable for Administrators.

## Copy the binaries
1. Download and extract on your local computer the latest version from http://msopentechstorage.blob.core.windows.net/windows-azure-solr/SolrInstWR-12182012.zip.

2. Please make sure that you unblock all the dll's and config files using instructions at http://msdn.microsoft.com/en-us/library/ee890038(VS.100).aspx. 

3. Download the publishSettings file for your Azure subscription. You can either run the Get-AzurePublishSettingsFile in a powershell window, or visit this link: https://windows.azure.com/download/publishprofile.aspx

4. Replace the Azure.publishSettings file in the folder where you unzipped the package with your own publishSettings file. Alternatively, you can simply delete the Azure.publishsettings file in the folder where you unzipped the package and run the installer. This will launch the browser asking you to download the publishsettings file.

5. Launch a command prompt (cmd.exe) as an administrator and cd to the local folder selected above.

## Run the installer:
    - Inst4WA.exe -XmlConfigPath "<yourpath>/SolrInstWR.xml" -DomainName "<youruniquename>" -Subscription "<yoursubscriptionname>" -Location "<datacenterlocation>"

6. Note that we currently support Solr 3.x as well as 4.x. The names of the SolrInstWR.xml files indicate the Solr version that will be installed using that config file.

## Administering Solr/Lucene

In the panel for your deployment you will find the DNS name `http://<Deployment_Endpoint>.cloudapp.net`

- Start in a browser `http://<Deployment_Endpoint>.cloudapp.net` to the typical tasks for Solr
- Crawl or Import data from the sample Windows Azure blob in the Crawl or Import tabs
- Validate that the index is replicated across SolrSlave  instances on the Home tab by checking the index size.
- After the index is replicated execute a search in the Search tab
