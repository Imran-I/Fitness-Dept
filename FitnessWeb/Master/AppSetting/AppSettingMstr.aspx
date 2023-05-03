<%@ Page Title="App Setting" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true"
    CodeFile="AppSettingMstr.aspx.cs" Inherits="Master_AppSetting" %>

<asp:Content ID="CntWorkoutType" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .ddl {
            padding: 1rem;
            border: 1px dashed;
            margin-bottom: 0.5rem;
            border-radius: 1rem;
        }

        .profile {
            margin-bottom: 0.5rem;
            color: #202124;
            font-size: 1rem;
            margin-left: 2rem;
        }

        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            border: 1px solid;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="App Setting"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>App <span>Setting</span></h5>
            </div>
            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                                <asp:DropDownList ID="ddlOwnerList" CssClass="form-select" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvgymOwner" InitialValue="0" ValidationGroup="AppSetting"
                                    ControlToValidate="ddlOwnerList" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Gym Owner">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtpackageName" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Package Name</asp:Label>
                                    <asp:RequiredFieldValidator ID="RfvpackageName" ValidationGroup="AppSetting"
                                        ControlToValidate="txtpackageName" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Package Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                                <asp:DropDownList ID="ddlappType" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="0" Text="App Type"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Android"></asp:ListItem>
                                    <asp:ListItem Value="W" Text="Web"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvappType" InitialValue="0" ValidationGroup="AppSetting"
                                    ControlToValidate="ddlappType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select App Type">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtappVersion" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">App Version</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvappVersion" ValidationGroup="AppSetting"
                                        ControlToValidate="txtappVersion" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter App Version">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtversionChanges" TextMode="MultiLine" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Version Changes </asp:Label>
                                    <asp:RequiredFieldValidator ID="RfvversionChanges" ValidationGroup="AppSetting"
                                        ControlToValidate="txtversionChanges" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Version Changes">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" runat="server" ValidationGroup="AppSetting" Text="Submit"
                    OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>App <span>Setting</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvAppSettingMstr" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>

                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="uniqueId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gym Owner" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgymOwnerName" runat="server" Text='<%#Bind("gymOwnerName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="App Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblappType" runat="server" Text='<%#Eval("appType").ToString() == "A"? "Android":"Web" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Package Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblpackageName" runat="server" Text='<%#Bind("packageName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="App Version" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblappVersion" runat="server" Text='<%#Bind("appVersion") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Version Changes" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblversionChanges" runat="server" Text='<%#Bind("versionChanges") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader" Visible="false">
                            <%--  <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25" 
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEdit_Click" />
                            </ItemTemplate>--%>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader" Visible="false">
                            <ItemTemplate>
                                <%--<asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                                --%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

    <asp:HiddenField ID="hfImageUrl" runat="server" />

</asp:Content>

