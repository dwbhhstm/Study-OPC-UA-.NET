using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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

            // "1 - Create a config"
            mConfig = createOpcUaAppConfiguration();
        }

        private ApplicationConfiguration createOpcUaAppConfiguration()
        {
            ApplicationConfiguration config = new ApplicationConfiguration()
            {
                ApplicationName = "OpcUaClient",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier(),
                    AutoAcceptUntrustedCertificates = true // 신뢰할 수 없는 인증서 허용
                },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 }
            };

            config.Validate(ApplicationType.Client);

            // 신뢰할 수 없는 인증서 허용
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, e) => e.Accept = e.Error.StatusCode == StatusCodes.BadCertificateUntrusted;
            }

            return config;
        }

        private async void btnConnect_ClickAsync(object sender, EventArgs e)
        {
            string endPointUrl = cbEndpoint.Text;

            try
            {
                // 2 - Create Session
                mSession = await Session.Create(mConfig, new ConfiguredEndpoint(null, new EndpointDescription(endPointUrl)), true, "", 60000, null, null);

                #region annotation
                //Security Mode
                //	None
                //	Sign
                //	Sign & Encrypt

                //Security Policy
                //	Basic128Rsa15
                //	Basic256
                //	Basic256Sha256
                //	Aes128Sha256RsaOaep
                //	Aes256Sha256RsaPss

                //Uri uri = new Uri(endPointUrl);

                //// 2 - Create EndpointDescription
                //EndpointDescription description = new EndpointDescription
                //{
                //    EndpointUrl = uri.ToString(),
                //    SecurityMode = MessageSecurityMode.Sign,
                //    SecurityPolicyUri = SecurityPolicies.Basic256Sha256,
                //    Server = new ApplicationDescription
                //    {
                //        ApplicationUri = Utils.UpdateInstanceUri(uri.ToString()),
                //        ApplicationName = uri.AbsolutePath
                //    }
                //};

                //// 3 - Create Endpoint
                //ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, description);

                //// 4 - Create Session
                //mSession = await Session.Create(mConfig, endpoint, true, "", 60000, null, null); 
                #endregion

                #region Show the server namespace
                // 3 - Show the server namespace
                browseTreeCtrl1.SetView(mSession, BrowseViewType.Objects, null);
                #endregion

                #region BrowsTreeControl을 사용하지 않는 경우 아래 코드를 사용하여 AddressSpace를 순회할 수 있다.
                // BrowsTreeControl을 사용하지 않는 경우 아래 코드를 사용하여 AddressSpace를 순회할 수 있다.
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
                #endregion
            }
            catch (Exception ex)
            {
                tbErrorMessage.Text = ex.Message;
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

            if (string.IsNullOrEmpty(tbNodeId01.Text))
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                tbResult01.Text = mSession.ReadValue(tbNodeId01.Text).Value.ToString();
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

        private void btn03_Click(object sender, EventArgs e)
        {
            #region annotation
            //#region validation
            //if (mSession == null)
            //{
            //    return;
            //}

            //if (lbNodeId02.Items.Count <= 0 || lbNodeId02.SelectedItems.Count <= 0)
            //{
            //    return;
            //}
            //#endregion

            //clearTextControls();

            //try
            //{
            //    var variableIds = new List<NodeId>();
            //    var expectedTypes = new List<Type>();
            //    foreach (object item in lbNodeId02.SelectedItems)
            //    {
            //        variableIds.Add(new NodeId(item.ToString()));
            //        expectedTypes.Add(typeof(object));
            //    }

            //    mSession.ReadValues(variableIds, expectedTypes, out List<object> values, out List<ServiceResult> errors);

            //    //lbResult02.DataSource = values;
            //    for (int i = 0; i < lbNodeId02.SelectedItems.Count; i++)
            //    {
            //        lbResult02.Items.Add($"{values[i]} ({lbNodeId02.SelectedItems[i]}, {expectedTypes[i].Name})");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tbErrorMessage.Text = ex.Message;
            //} 
            #endregion
            #region annotation-2
            //try
            //{
            //    var nodesToRead = new ReadValueIdCollection();
            //    foreach (object item in lbNodeId02.SelectedItems)
            //    {
            //        nodesToRead.Add(new ReadValueId
            //        {
            //            NodeId = new NodeId(item.ToString()),
            //            AttributeId = Attributes.Value
            //        });
            //    }

            //    // read the attributes.
            //    mSession.Read(
            //        null,
            //        0,
            //        TimestampsToReturn.Neither,
            //        nodesToRead,
            //        out DataValueCollection results,
            //        out DiagnosticInfoCollection diagnosticInfos);

            //    ClientBase.ValidateResponse(results, nodesToRead);
            //    ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

            //    // check for error.
            //    if (StatusCode.IsBad(results[0].StatusCode))
            //    {
            //        ValueTB.Text = results[0].StatusCode.ToString();
            //        ValueTB.ForeColor = Color.Red;
            //        ValueTB.Font = new Font(ValueTB.Font, FontStyle.Bold);
            //        return;
            //    }

            //    SetValue(results[0].WrappedValue);
            //}
            //catch (Exception exception)
            //{
            //    ClientUtils.HandleException(Text, exception);
            //} 
            #endregion

            #region validation
            if (mSession == null)
            {
                return;
            }

            if (lbNodeId02.Items.Count <= 0 || lbNodeId02.SelectedItems.Count <= 0)
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                var items = new List<string>();
                foreach (object item in lbNodeId02.SelectedItems)
                {
                    items.Add(item.ToString());
                }

                read(items);
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(Text, ex);
                //tbErrorMessage.Text = ex.Message;
            }
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            #region validation
            if (mSession == null)
            {
                return;
            }

            if (lbNodeId02.Items.Count <= 0 || lbNodeId02.SelectedItems.Count <= 0)
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                var items = new List<string>();
                foreach (object item in lbNodeId02.SelectedItems)
                {
                    items.Add(item.ToString());
                }

                write(items, int.Parse(tbValue01.Text));

                read(items);
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(Text, ex);
                //tbErrorMessage.Text = ex.Message;
            }
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            #region validation
            if (mSession == null)
            {
                return;
            }

            if (lbNodeId02.Items.Count <= 0)
            {
                return;
            }
            #endregion

            clearTextControls();

            try
            {
                var items = new List<string>();
                foreach (object item in lbNodeId02.Items)
                {
                    items.Add(item.ToString());
                }
                items = items.Where(x => x != string.Empty).ToList();

                write(items, 1);

                read(items);
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException(Text, ex);
                //tbErrorMessage.Text = ex.Message;
            }
        }

        private void clearTextControls()
        {
            tbResult01.Text = string.Empty;
            tbErrorMessage.Text = string.Empty;

            //lbResult01.DataSource = null;
            lbResult01.Items.Clear();

            //lbResult02.DataSource = null;
            lbResult02.Items.Clear();
        }

        private void read(List<string> items)
        {
            var nodesToRead = new ReadValueIdCollection();
            foreach (string item in items)
            {
                nodesToRead.Add(new ReadValueId
                {
                    NodeId = new NodeId(item),
                    AttributeId = Attributes.Value
                });
            }

            // read the attributes.
            ResponseHeader responseHeader = mSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);

            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

            // check for error.
            for (int i = 0; i < results.Count; i++)
            {
                if (StatusCode.IsBad(results[i].StatusCode))
                {
                    throw ServiceResultException.Create(results[i].StatusCode, 0, diagnosticInfos, responseHeader.StringTable);
                }
            }

            // load.
            for (int i = 0; i < results.Count; i++)
            {
                (string, BuiltInType) result = getValue(results[i].WrappedValue);
                lbResult02.Items.Add($"{result.Item1} ({items[i]}, {result.Item2})");
            }
        }

        private void write(List<string> items, int value)
        {
            var nodesToWrite = new WriteValueCollection();
            foreach (string item in items)
            {
                nodesToWrite.Add(new WriteValue
                {
                    NodeId = new NodeId(item),
                    AttributeId = Attributes.Value,
                    Value = new DataValue(getValue(value++, item))
                });
            }

            // write the attributes.
            ResponseHeader responseHeader = mSession.Write(
                null,
                nodesToWrite,
                out StatusCodeCollection results,
                out DiagnosticInfoCollection diagnosticInfos);

            ClientBase.ValidateResponse(results, nodesToWrite);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToWrite);

            // check for error.
            for (int i = 0; i < results.Count; i++)
            {
                if (StatusCode.IsBad(results[i]))
                {
                    throw ServiceResultException.Create(results[i], 0, diagnosticInfos, responseHeader.StringTable);
                }
            }
        }

        #region annotation
        //private string getValue(Variant value)
        //{
        //    // check for null.
        //    if (value == Variant.Null)
        //    {
        //        return string.Empty;
        //    }

        //    // get the source type.
        //    TypeInfo sourceType = value.TypeInfo;
        //    if (sourceType == null)
        //    {
        //        sourceType = TypeInfo.Construct(value.Value);
        //    }

        //    return value.Value.ToString();
        //} 
        #endregion
        private (string, BuiltInType) getValue(Variant value)
        {
            // check for null.
            if (value == Variant.Null)
            {
                return (null, BuiltInType.Null);
            }

            // get the source type.
            TypeInfo sourceType = value.TypeInfo;
            if (sourceType == null)
            {
                sourceType = TypeInfo.Construct(value.Value);
            }

            return (value.Value.ToString(), sourceType.BuiltInType);
        }

        private Variant getValue(object value, string nodeId)
        {
            // check for null.
            if (string.IsNullOrEmpty(value?.ToString()) || string.IsNullOrEmpty(nodeId))
            {
                return new Variant(new StatusCode(0x80000000)); // 0x80000000: ('Bad', 'The operation failed.')
            }

            return new Variant(TypeInfo.Cast(value, mSession.ReadValue(new NodeId(nodeId)).WrappedValue.TypeInfo.BuiltInType));
        }
    }
}