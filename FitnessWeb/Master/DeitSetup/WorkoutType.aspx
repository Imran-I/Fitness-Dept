<%@ Page Title="Workout Type" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="WorkoutType.aspx.cs" Inherits="Master_WorkoutType" %>

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
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
             <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Workout Type"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Workout <span>Type</span></h5>
            </div>
            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <asp:DropDownList ID="ddlWorkoutType" TabIndex="1" CssClass="form-select" runat="server">                                    
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvTaxType" InitialValue="0" ValidationGroup="WorkoutType" ControlToValidate="ddlWorkoutType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Workout Type">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtWorkOutType" TabIndex="2" AutoComplete="off"  CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">WorkOut Category</asp:Label>
                                     <asp:RequiredFieldValidator ID="RfvWorkOutType" ValidationGroup="WorkoutType" ControlToValidate="txtWorkOutType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Workout Category">
                                </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtDescription" TabIndex="3"   AutoComplete="off" TextMode="MultiLine" MaxLength="150"
                                        CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Description </asp:Label>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtVideoUrl" TextMode="MultiLine" TabIndex="4"  AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Video Url</asp:Label>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtcalories" TabIndex="3"   AutoComplete="off" onkeypress="return isNumber(event);"  MaxLength="20"
                                        CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Calories </asp:Label>
                                      <asp:RequiredFieldValidator ID="rfvCalories" ValidationGroup="WorkoutType" ControlToValidate="txtcalories" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Workout Calories">
                                </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-4">
                                <img id="imgpreview" class="imgpreview" TabIndex="5" ClientIDMode="Static" runat="server" src="../../img/Defaultupload.png" />
                                <asp:FileUpload ID="fuimage" CssClass="mx-4" runat="server" onchange="showpreview(this);" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="float-end">
                <asp:Button  ID="btnSubmit" CssClass="btnSubmit" TabIndex="6" ValidationGroup="WorkoutType" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="7" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Workout <span>Type</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>
          
             <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvWorkoutTypeMstr" runat="server" DataKeyNames="workoutTypeId" AutoGenerateColumns="false" 
                    CssClass ="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="WorkoutTypeId"  HeaderStyle-HorizontalAlign="Center" Visible="false"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutTypeId" runat="server" Text='<%#Bind("workoutTypeId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="WorkoutCatTypeId"  HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Workout Type Name"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutTypeName" runat="server" Text='<%#Bind("workoutTypeName") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Workout Type"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutType" runat="server" Text='<%#Bind("workoutType") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Calories"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcalories" runat="server" Text='<%#Bind("calories") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Description"  HeaderStyle-HorizontalAlign="Center" Visible="false"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ImageUrl"  HeaderStyle-HorizontalAlign="Center" Visible="false"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Video"  HeaderStyle-HorizontalAlign="Center"  Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblvideo" runat="server" Text='<%#Bind("video") %>' ></asp:Label>
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
        function showpreview(input) {

            //if (input.files && input.files[0]) {

            //    var reader = new FileReader();
            //    reader.onload = function (e) {
            //        $('#imgpreview').attr('src', e.target.result);
            //    }
            //    reader.readAsDataURL(input.files[0]);
            //}
            var fup = document.getElementById("<%=fuimage.ClientID %>");
               var fileName = fup.value;
               var maxfilesize = 1024 * 1024;
               filesize = input.files[0].size;
               var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG"|| ext == "svg" || ext == "SVG") {
                   if (filesize <= maxfilesize) {
                       if (input.files && input.files[0]) {
                           var reader = new FileReader();
                           reader.onload = function (e) {
                               $('#<%=imgpreview.ClientID%>').prop('src', e.target.result);

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

