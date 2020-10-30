using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Sample.Controls;
using System;
using System.Collections.Generic;
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

        private void btn01_Click(object sender, EventArgs e)
        {
            #region validation
            if (mSession == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tbNodeId01.Text))
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                tbResult01.Text = mSession.ReadValue(tbNodeId01.Text.Trim()).Value.ToString();
            }
            catch (Exception ex)
            {
                tbErrorMessage.Text = ex.Message;
            }
        }

        private void btn02_Click(object sender, EventArgs e)
        {
            #region annotation(reference)
            //// Snippet 1
            //var session = await Session.Create(config, endpoint, false, "OPC UA Console Client", 60000, new UserIdentity(new AnonymousIdentityToken()), null);
            //while (true)
            //{
            //    session.ReadValues(new List<NodeId>() { nodeId }, new List<Type>() { typeof(string) }, out var values, out var errors);
            //    Thread.Sleep(3000);
            //    System.Console.WriteLine($"Value: {values[0]}");
            //}

            //// Snippet 2
            //while (true)
            //{
            //    session.ReadValues(new List<NodeId>() { nodeId }, new List<Type>() { typeof(string) }, out var values, out var errors);
            //    var dv = session.ReadValue(nodeId);
            //    System.Console.WriteLine($"Value: {values[0]}");
            //    System.Console.WriteLine($"Single Read Value: {dv.Value}");
            //    Thread.Sleep(3000);
            //} 

            //mSession.ReadValues(new List<NodeId>() { new NodeId("ns=3;i=1009") }, new List<Type>() { typeof(object) }, out var values, out var errors);

            //var variableIds = new List<NodeId>
            //{
            //    new NodeId("ns=3;i=1009"),
            //    new NodeId("ns=3;i=1010"),
            //    new NodeId("ns=3;i=1011"),
            //    new NodeId("ns=3;i=1012"),
            //    new NodeId("ns=3;i=1013"),
            //    new NodeId("ns=3;i=1014"),
            //    new NodeId("ns=3;i=1015"),
            //    new NodeId("ns=3;i=1016"),
            //    new NodeId("ns=3;i=1017"),
            //    new NodeId("ns=3;i=1018"),
            //    new NodeId("ns=3;i=1019"),
            //    new NodeId("ns=3;i=1020"),
            //    new NodeId("ns=3;i=1021"),
            //    new NodeId("ns=3;i=1022"),
            //    new NodeId("ns=3;i=1023"),
            //    new NodeId("ns=3;i=1024"),
            //    new NodeId("ns=3;i=1025"),
            //    new NodeId("ns=3;i=1026"),
            //    new NodeId("ns=3;i=1027"),
            //    new NodeId("ns=3;i=1028"),
            //    new NodeId("ns=3;i=1029"),
            //    new NodeId("ns=3;i=1030"),
            //    new NodeId("ns=3;i=1031"),
            //    new NodeId("ns=3;i=1032"),
            //    new NodeId("ns=3;i=1033"),
            //    new NodeId("ns=3;i=1034"),
            //    new NodeId("ns=3;i=1035"),
            //    new NodeId("ns=3;i=1036"),
            //    new NodeId("ns=3;i=1037"),
            //    new NodeId("ns=3;i=1038"),
            //};

            //var expectedTypes = new List<Type>();
            //for (int i = 0; i < variableIds.Count; i++)
            //{
            //    expectedTypes.Add(typeof(object));
            //}

            //mSession.ReadValues(variableIds, expectedTypes, out List<object> values, out List<ServiceResult> errors);

            //lbResult01.DataSource = values;
            #endregion

            #region validation
            if (mSession == null)
            {
                return;
            }

            if (lbNodeId01.Items.Count <= 0 || lbNodeId01.SelectedItems.Count <= 0)
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                var variableIds = new List<NodeId>();
                var expectedTypes = new List<Type>();
                foreach (object item in lbNodeId01.SelectedItems)
                {
                    variableIds.Add(new NodeId(item.ToString()));
                    expectedTypes.Add(typeof(object));
                }

                mSession.ReadValues(variableIds, expectedTypes, out List<object> values, out List<ServiceResult> errors);

                //lbResult01.DataSource = values;
                for (int i = 0; i < lbNodeId01.SelectedItems.Count; i++)
                {
                    lbResult01.Items.Add($"{values[i]} ({lbNodeId01.SelectedItems[i]}, {expectedTypes[i].Name})");
                }
            }
            catch (Exception ex)
            {
                tbErrorMessage.Text = ex.Message;
            }
        }

        private void clearTextControls()
        {
            tbResult01.Text = string.Empty;
            tbErrorMessage.Text = string.Empty;

            //lbResult01.DataSource = null;
            lbResult01.Items.Clear();
        }
    }
}