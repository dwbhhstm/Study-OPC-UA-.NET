using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Sample.Controls;
using System;
using System.Windows.Forms;

namespace NugetClient
{
    public partial class MinimalClient : Form
    {
        private readonly ApplicationConfiguration mConfig;
        private Session mSession;

        public MinimalClient()
        {
            InitializeComponent();

            if (cbEndpoint.Items.Count > 0)
            {
                cbEndpoint.SelectedIndex = 0;
            }

            //"1 - Create a config"
            mConfig = createOpcUaAppConfiguration();
        }

        private ApplicationConfiguration createOpcUaAppConfiguration()
        {
            ApplicationConfiguration config = new ApplicationConfiguration()
            {
                ApplicationName = "MinimalClient",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier(),
                    AutoAcceptUntrustedCertificates = true   //신뢰할 수 없는 인증서 허용
                },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 }
            };

            config.Validate(ApplicationType.Client);

            //신뢰할 수 없는 인증서 허용
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, e) => e.Accept = e.Error.StatusCode == StatusCodes.BadCertificateUntrusted;
            }

            return config;
        }

        private async void btnConnect_ClickAsync(object sender, EventArgs e)
        {
            string endPointUrl = cbEndpoint.Text;

            //2 - Create Session
            mSession = await Session.Create(mConfig, new ConfiguredEndpoint(null, new EndpointDescription(endPointUrl)), true, "", 60000, null, null);

            //3 - Show the server namespace
            browseTreeCtrl1.SetView(mSession, BrowseViewType.Objects, null);

            //BrowsTreeControl을 사용하지 않는 경우 아래 코드를 사용하여 AddressSpace를 순회할 수 있다.
            mSession.Browse(
                null,
                null,
                ObjectIds.ObjectsFolder,
                0u,
                BrowseDirection.Forward,
                ReferenceTypeIds.HierarchicalReferences,
                true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                out _,
                out ReferenceDescriptionCollection refs);

            foreach (ReferenceDescription rd in refs)
            {
                Console.WriteLine($"\n{rd.DisplayName}: {rd.BrowseName}, {rd.NodeClass}");

                mSession.Browse(
                    null,
                    null,
                    ExpandedNodeId.ToNodeId(rd.NodeId, mSession.NamespaceUris),
                    0u,
                    BrowseDirection.Forward,
                    ReferenceTypeIds.HierarchicalReferences,
                    true,
                    (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                    out byte[] nextCp,
                    out ReferenceDescriptionCollection nextRefs);

                foreach (ReferenceDescription nextRd in nextRefs)
                {
                    Console.WriteLine($"+ { nextRd.DisplayName}: {nextRd.BrowseName}, {nextRd.NodeClass}");
                }
            }
        }

        private void minimalClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnect();
        }

        private void disconnect()
        {
            if (mSession != null)
            {
                mSession.Close();
                mSession = null;
            }
        }
    }
}