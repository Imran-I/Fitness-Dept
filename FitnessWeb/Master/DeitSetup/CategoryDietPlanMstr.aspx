<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="CategoryDietPlanMstr.aspx.cs" Inherits="Master_DeitSetup_WorkOutFoodMenuMstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .CheckBoxList {
            font-size: 0.7rem;
            white-space: nowrap;
            display: flex;
            justify-content: space-between;
            font-weight: 900;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Category WorkOut Plan Master"></asp:Label>
        </div>
    </div>

    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Category Diet<span> Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlWorkOutType" CssClass="form-select" runat="server" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvmealType" InitialValue="0" ValidationGroup="Food" ControlToValidate="ddlWorkOutType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Category">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3" id="divAddDtl" runat="server">
                    <asp:DataList ID="dtlFoodItem" runat="server" OnItemDataBound="dtlFoodItem_ItemDataBound">
                        <ItemTemplate>
                            <asp:Label ID="lblMealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>'
                                Style="font-size: 15px; font-weight: 800;"> </asp:Label>
                            <asp:Label ID="lblMealType" runat="server" Text='<%#Bind("mealType") %>' Visible="false"> </asp:Label>
                            <asp:DataList ID="dtlFoodItemList" runat="server" RepeatColumns="2">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="chckfoodItem" CssClass="CheckBoxList"
                                        TabIndex="2" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                    <asp:Label ID="lblDietTimeID" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lblMealtypeID" runat="server" Text='<%#Bind("mealType") %>' Visible="false"> </asp:Label>

                                </ItemTemplate>
                            </asp:DataList>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3" id="divEditDtl" runat="server">
                    <asp:DataList ID="dtlEditFoodItem" runat="server" OnItemDataBound="dtlEditFoodItem_ItemDataBound">
                        <ItemTemplate>
                            <asp:Label ID="lblMealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>'
                                Style="font-size: 15px; font-weight: 800;"> </asp:Label>
                            <asp:Label ID="lblmealTypeId" runat="server" Text='<%#Bind("mealTypeId") %>' Visible="false"> </asp:Label>
                            <asp:DataList ID="dtlFoodItemLists" runat="server" RepeatColumns="2">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="chckfoodItem" CssClass="CheckBoxList"
                                        TabIndex="2" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                    <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lblfoodItemId" runat="server" Text='<%#Bind("foodItemId") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lbldietTimeId" runat="server" Text='<%#Bind("dietTimeId") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"> </asp:Label>
                                    <asp:Label ID="lblactiveStatus" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"> </asp:Label>

                                </ItemTemplate>
                            </asp:DataList>
                        </ItemTemplate>
                    </asp:DataList>


                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" TabIndex="3" CssClass="btnSubmit" ValidationGroup="Food" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="4" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category Diet<span> Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvCategoryDietPlanMstr" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>

