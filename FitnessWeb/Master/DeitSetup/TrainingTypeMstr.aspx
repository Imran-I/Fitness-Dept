<%@ Page Title="Training Type" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainingTypeMstr.aspx.cs" Inherits="Master_TrainingTypeMstr" %>

<asp:Content ID="CtTraningType" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .empdtls {
            margin-bottom: 1.7rem;
            color: #202124;
            font-size: 1rem;
        }

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
            margin-top: 1rem;
            border: 1px solid;
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
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Training Type Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Training Type <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <asp:DropDownList ID="ddlTrainingType" TabIndex="1" CssClass="form-select" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvTraningType" ValidationGroup="TraningTypeMstr"
                                    ControlToValidate="ddlTrainingType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Training Type" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtDescription" TabIndex="2" AutoComplete="Off" runat="server" CssClass="txtbox" TextMode="MultiLine" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Description</asp:Label>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                      <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                      <asp:Image id="imgTrainingPhotoPrev" class="imgpreview"  ClientIDMode="Static" runat="server" ImageUrl="../../img/Defaultupload.png" />
                        <asp:FileUpload ID="Fuimage" CssClass="mx-4" TabIndex="3" runat="server" onchange="ShowImagePreview(this);" />
                    </div>
                </div>
            </div>

            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" TabIndex="4" runat="server" Text="Submit" ValidationGroup="TraningTypeMstr" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="5" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Training Type <span>Master</span></h4>
                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                            <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                 <asp:GridView ID="gvTrainingMstr" runat="server" DataKeyNames="trainingTypeId" AutoGenerateColumns="false" 
                     CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Training Type Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            <ItemTemplate>
                                <asp:Label ID="lbltrainingTypeId" runat="server" Text='<%#Bind("trainingTypeId") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lbltrainingTypeNameId" runat="server" Text='<%#Bind("trainingTypeNameId") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>' Visible="true"></asp:Label>                               
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description"  HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="ImageUrl"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false"> 
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>' Visible="false"></asp:Label>      
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
    <asp:HiddenField ID="hfImageUrl" runat="server" />
       <script type="text/javascript">
           function ShowImagePreview(input) {
               
               var fup = document.getElementById("<%=Fuimage.ClientID %>");
               var fileName = fup.value;
               var maxfilesize = 1024 * 1024;
               filesize = input.files[0].size;
               var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
               if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                   if (filesize <= maxfilesize) {
                       if (input.files && input.files[0]) {
                           var reader = new FileReader();
                           reader.onload = function (e) {
                               $('#<%=imgTrainingPhotoPrev.ClientID%>').prop('src', e.target.result);
                          
                        };
                        reader.readAsDataURL(input.files[0]);
                      
                        }
                    }
                    else {
                        swal("Please, Upload image file less than or equal to 1 MB !!!");
                        fup.focus();
                        return false;
                    }
                }
                else {
                    swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                    fup.focus();
                    return false;
                }
            }

        
       </script>
</asp:Content>

