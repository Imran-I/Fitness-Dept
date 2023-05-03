<%@ Page Title="Food Item  Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FooditemMstr.aspx.cs" Inherits="Master_FooditemMstr" %>

<asp:Content ID="CntFooditemMstr" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Food Menu Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Food Item  Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Food Item <span>Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFoodItemName" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="1" />
                        <asp:Label CssClass="txtlabel" runat="server">Food Item Name <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvFoodItemName" ValidationGroup="Fooditem" ControlToValidate="txtFoodItemName" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Food Item Name">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddldietType" CssClass="form-select" runat="server" TabIndex="2">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvdietType" InitialValue="0" ValidationGroup="Fooditem" ControlToValidate="ddldietType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Diet Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlUnit" CssClass="form-select" runat="server" TabIndex="3"> 
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvUnit" InitialValue="0" ValidationGroup="Fooditem" ControlToValidate="ddlUnit" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Unit Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlservingIn" CssClass="form-select" runat="server" TabIndex="4">
                        <asp:ListItem Value="0">Serving In *</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvservingIn" InitialValue="0" ValidationGroup="Fooditem" ControlToValidate="ddlservingIn" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Serving In">
                    </asp:RequiredFieldValidator>
                </div>                
            </div>
            <div class="row">
                <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtprotein" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="5" />
                        <asp:Label CssClass="txtlabel" runat="server">Protein <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvprotein" ValidationGroup="Fooditem" ControlToValidate="txtprotein" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Protein">
                    </asp:RequiredFieldValidator>
                </div>
                      <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtcarbs" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="6" />
                        <asp:Label CssClass="txtlabel" runat="server">Carbs <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvcarbs" ValidationGroup="Fooditem" ControlToValidate="txtcarbs" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Carbs">
                    </asp:RequiredFieldValidator>
                </div>
                      <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtfat" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="7" />
                        <asp:Label CssClass="txtlabel" runat="server">Fat <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvfat" ValidationGroup="Fooditem" ControlToValidate="txtfat" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Fat">
                    </asp:RequiredFieldValidator>
                </div>
                     <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtcalories" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="8" />
                        <asp:Label CssClass="txtlabel" runat="server">Calories <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvcalories" ValidationGroup="Fooditem" ControlToValidate="txtcalories" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Calories">
                    </asp:RequiredFieldValidator>
                </div>
                 <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-4">
                     <div class="profile">Food Image</div>
                    <img id="imgpreview" class="imgpreview" ClientIDMode="Static" runat="server" TabIndex="9" src="~/img/Defaultupload.png" />
                    <asp:FileUpload ID="fuimage" CssClass="mx-4" runat="server" TabIndex="9" onchange="showpreview(this);" />
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btnSubmit" TabIndex="10" ValidationGroup="Fooditem" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" TabIndex="11" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Food Item  <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                 <asp:GridView ID="gvFoodItemMstr" runat="server" DataKeyNames="foodItemId" AutoGenerateColumns="false" 
                    CssClass ="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FoodItem Id" Visible="false"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblfoodItemId" runat="server" Text='<%#Bind("foodItemId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="DietType Id" Visible="false"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lbldietTypeId" runat="server" Text='<%#Bind("dietTypeId") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Food Item Name"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblfoodItemName" runat="server" Text='<%#Bind("foodItemName") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblunit" runat="server" Text='<%#Bind("unit") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serving-In"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblservingIn" runat="server" Text='<%#Bind("servingIn") %>' Visible="false"></asp:Label>   
                                 <asp:Label ID="Label1" runat="server" Text='<%#Bind("servingInName") %>'></asp:Label>   
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Protein"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblprotein" runat="server" Text='<%#Bind("protein") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fat"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblfat" runat="server" Text='<%#Bind("fat") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calories"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblcalories" runat="server" Text='<%#Bind("calories") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carbs"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblcarbs" runat="server" Text='<%#Bind("carbs") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="ImageUrl" Visible="false"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25" 
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEdit_Click" />
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function showpreview(input) {

            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }

        }

    </script>
</asp:Content>

