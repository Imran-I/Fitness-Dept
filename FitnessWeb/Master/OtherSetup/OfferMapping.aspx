<%@ Page Title="Offer Mapping" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="OfferMapping.aspx.cs" Inherits="Master_OfferMapping" %>

<asp:Content ID="CntOfferMapping" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .labels {
            font-weight: bold;
            color: #5f6064;
            font-size: 1rem;
            margin-left: 10px;
        }

        legend {
            display: block;
            width: inherit;
            max-width: 100%;
            padding: 0px 5px;
            font-size: inherit;
            font-weight: bold;
            line-height: inherit;
            color: inherit;
            white-space: normal;
        }

        .legBorder {
            border-width: 1px;
            border-color: #00000061;
            border-radius: 6px;
            border-style: dashed;
            padding: 6px;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Offer Mapping"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="divForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Offer <span>Mapping</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlBranchList" CssClass="form-select" runat="server" TabIndex="1" Enabled="false">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvBranchList" InitialValue="0" ValidationGroup="OfferMapping" ControlToValidate="ddlBranchList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Branch">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlOfferList" CssClass="form-select" runat="server" TabIndex="2"
                        OnSelectedIndexChanged="ddlOfferList_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvOfferlist" InitialValue="0" ValidationGroup="OfferMapping" ControlToValidate="ddlOfferList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div id="divOfferDetails" runat="server" visible="false">
                <fieldset class="legBorder mb-3">
                    <legend>Offer Details</legend>
                    <asp:Label ID="lblOfferNameheading" runat="server" Font-Bold="true" CssClass="labels" Visible="false"
                        Style="font-size: 25px"></asp:Label>

                    <div class="row">
                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                            <div class="row">
                                <div class="col-sm-5 col-md-5 col-lg-5 col-xs-5">
                                    <label for="lblmaxcharge" class="labels">
                                        Description :
                                    </label>
                                </div>
                                <div class="col-sm-7 col-md-7 col-lg-7 col-xs-7 ">
                                    <asp:Label ID="lblOfferDes" runat="server" Font-Bold="true"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                            <div class="row">
                                <div class="col-sm-5 col-md-5 col-lg-5 col-xs-5">
                                    <label for="lblmaxcharge" class="labels" style="margin-left: 0px;">
                                        Offer Value<span id="PerorFix" runat="server"
                                            style="font-size: 10px; color: black; padding-left: 6px"></span> :
                                    </label>
                                </div>
                                <div class="col-sm-7 col-md-7 col-lg-7 col-xs-7">
                                    <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                            <div class="row">
                                <div class="col-sm-5 col-md-5 col-lg-5 col-xs-5">
                                    <label for="lblmaxcharge" class="labels">
                                        Offer Code :
                                    </label>
                                </div>
                                <div class="col-sm-7 col-md-7 col-lg-7 col-xs-7 ">
                                    <asp:Label ID="lblOffCode" runat="server" Font-Bold="true"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                            <div class="row">
                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-3">
                                    <label  style="margin-left: 0px;">
                                        Min. Amt<span id="Span1" runat="server"
                                            style="font-size: 10px; color: black; padding-left: 6px"></span> :
                                    </label>
                                </div>
                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-3">
                                    ₹<asp:Label ID="lblMinAmt" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-3">
                                    <label style="margin-left: 0px;">
                                        Max. Amt<span id="Span2" runat="server"
                                            style="font-size: 10px; color: black; padding-left: 6px"></span> :
                                    </label>
                                </div>
                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-3">
                                    ₹<asp:Label ID="lblMaxAmt" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <fieldset class="legBorder m-3" style="border-style: dotted;">
                        <legend>Validity</legend>
                        <div class="row">
                            <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-5 col-md-5 col-lg-5 col-xs-5">
                                        <label for="lblmaxcharge" class="labels">
                                            From :
                                        </label>
                                    </div>

                                    <div class="col-sm-7 col-md-7 col-lg-7 col-xs-7 ">
                                        <asp:Label ID="lblfromdate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6 col-lg-6 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-5 col-md-5 col-lg-5 col-xs-5">
                                        <label for="lblmaxcharge" class="labels">
                                            To :
                                        </label>
                                    </div>
                                    <div class="col-sm-7 col-md-7 col-lg-7 col-xs-7 ">
                                        <asp:Label ID="lblTodate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="row" id="Rules" runat="server">
                        <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12">
                            <div class="row">
                                <div class="col-sm-3 col-md-3 col-lg-3 col-xs-12">
                                    <label for="lblmaxcharge" class="labels">
                                        Rules :
                                    </label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12 mb-2">
                                    <asp:DataList ID="dtlRules" RepeatColumns="1"
                                        RepeatDirection="Vertical" runat="server" Width="100%">

                                        <ItemTemplate>
                                            <div class="col-12" style="height: 22px">
                                                <span style="font-size: 16px; color: #2196f3;" class="mdi mdi-star"></span>
                                                <asp:Label ID="lblRulestext" runat="server"
                                                    Visible="true" Text='<%# Eval("offerRule") %>' Font-Bold="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>

                                    </asp:DataList>
                                </div>
                            </div>
                        </div>

                    </div>
                </fieldset>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" runat="server" Text="Submit" TabIndex="3" OnClick="btnSubmit_Click"
                    ValidationGroup="OfferMapping" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" TabIndex="4" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Offer <span>Mapping</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">

                <asp:GridView
                    ID="gvOfferMapping"
                    runat="server"
                    Visible="true"
                    AutoGenerateColumns="false"
                    DataKeyNames="offerMappingId"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OfferMapping Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvOfferMappingId"
                                    runat="server"
                                    Text='<%#Bind("offerMappingId") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Branch" Visible="false">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvBranchId"
                                    runat="server"
                                    Text='<%#Bind("branchId") %>'>
                                </asp:Label>
                                <asp:Label
                                    ID="lblGvBranchName"
                                    runat="server"
                                    Text='<%#Bind("branchName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Offer Name">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvOfferId"
                                    runat="server" Visible="false"
                                    Text='<%#Bind("offerId") %>'>
                                </asp:Label>
                                <asp:Label
                                    ID="lblGvOfferHeading"
                                    runat="server"
                                    Text='<%#Bind("offerHeading") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Offer Value" Visible="true">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvofferType"
                                    runat="server" Visible="false"
                                    Text='<%#Eval("offerType").ToString() == "P" ? "Percentage":"Fixed" %>'>
                                </asp:Label>
                                <asp:Label
                                    ID="lblGvofferValue"
                                    runat="server" Visible="false"
                                    Text='<%#Bind("offerValue")%>'>
                                </asp:Label>
                                <asp:Label
                                    ID="lblGvofferValueType"
                                    runat="server"
                                    Text='<%#Eval("offerValue").ToString() + ""+ (Eval("offerType").ToString() == "P" ? "(%)":"(₹)") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Min. Amt" Visible="true" >
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvminAmt"
                                    runat="server"
                                    Text='<%#Bind("minAmt") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Max. Amt" Visible="true">
                            <ItemTemplate>
                                <asp:Label
                                    ID="lblGvmaxAmt"
                                    runat="server"
                                    Text='<%#Bind("maxAmt") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                 <asp:LinkButton ID="lnkView" runat="server" 
                                      Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="lnkView_Click">
                                    <i class="fa fa-eye" style="color:black"></i>
                                </asp:LinkButton>
                             
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>'
                                    OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>

