<%@ Page Title="Working Days" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="BranchWorkingDaysMstr.aspx.cs" Inherits="Master_BranchWorkOutDays" %>

<asp:Content ID="CtWorkOutDays" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ddl {
            padding: 1rem;
            border: 1px dashed;
            margin-bottom: 0.5rem;
            border-radius: 1rem;
        }


        .Daybtn {
            background-color: #e9ecef;
            border-radius: 25px;
            color: black;
            cursor: pointer;
            margin: 7px;
            box-shadow: rgba(17, 17, 26, 0.1) 0px 4px 16px, rgba(17, 17, 26, 0.05) 0px 8px 32px;
            display: block;
            font-family: CerebriSans-Regular,-apple-system,system-ui,Roboto,sans-serif;
            padding: 7px 10px;
            text-align: center;
            text-decoration: none;
            transition: all 250ms;
            border: 1px solid;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

            .Daybtn:hover {
                transform: scale(1.05);
            }
           
             .DaybtnNone {
           
            display: none;
           
        }
        .DaybtnClick {
            background-color: #6b6b6b;
            border-radius: 25px;
            color: white;
            cursor: pointer;
            margin: 7px;
            box-shadow: rgba(17, 17, 26, 0.1) 0px 4px 16px, rgba(17, 17, 26, 0.05) 0px 8px 32px;
            display: block;
            font-family: CerebriSans-Regular,-apple-system,system-ui,Roboto,sans-serif;
            padding: 7px 10px;
            text-align: center;
            text-decoration: none;
            transition: all 250ms;
            border: 0;
            -webkit-user-select: none;
            touch-action: manipulation;
        }

            .DaybtnClick:hover {
                transform: scale(1.05);
            }

        .lblSameTime {
            font-size: 13px;
            font-weight: 900;
        }

            .lblSameTime:hover {
                transform: scale(1.05);
            }

        .lblSameTimeClick {
            font-size: 13px;
            font-weight: 900;
            color: darkred;
            text-decoration: underline;
        }
    </style>

    <div class="PageRoute">
        <div>
             <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
             <asp:Label ID="Label2" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Branch Working Days"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Branch Working <span>Days</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="mb-3">
                        <div id="sameWorking" runat="server">
                        <div>
                            <asp:Label
                                ID="lblWorkingDays"
                                runat="server">Working Days<span class="reqiredstar">*</span>
                            </asp:Label>
                        </div>
                        <asp:Label runat="server" ID="lblWorkingDaysValue" Visible="false"></asp:Label>
                        <div class="mt-3 mb-4 ml-5" >
                            <asp:Label ID="Label1" CssClass="lblSameTime" runat="server"> 
                                      *  Does All days are Working days ?</asp:Label>
                            <asp:LinkButton ID="LnkSameTime" CssClass="lblSameTimeClick" runat="server"
                                OnClick="LnkSameTime_Click">Click Here</asp:LinkButton>
                        </div>
                          </div>
                        <asp:CheckBoxList TabIndex="1" ID="chkWorkingDays" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                            OnSelectedIndexChanged="chkWorkingDays_SelectedIndexChanged">
                            <asp:ListItem class="Daybtn" Value="1">sunday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="2">monday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="3">tuesday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="4">wednesday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="5">thursday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="6">friday</asp:ListItem>
                            <asp:ListItem class="Daybtn" Value="7">saturday</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>

                    <div class="row">
                         <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                            <div class="txtboxdiv" style="font-size: 18px;">
                               <asp:CheckBox ID="ChkHoliday" TabIndex="2"  runat="server" Text="IsHoliday" />
                            </div>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtFromTime" TabIndex="3" runat="server" CssClass="txtbox timePicker" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">From Time<span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="rfdFromTime" ValidationGroup="WorkingMstr"
                                    ControlToValidate="txtFromTime" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter From Time">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtToTime" TabIndex="4" runat="server" CssClass="txtbox timePicker" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">To Time<span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="rfdToTime" ValidationGroup="WorkingMstr"
                                    ControlToValidate="txtToTime" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter To Time">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                </div>

            </div>


            <div class="float-end">
                <asp:Button CssClass="btnSubmit" TabIndex="5" ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="WorkingMstr" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" TabIndex="6" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>


        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Branch Working  <span>Days</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvWorkingMstr" runat="server" DataKeyNames="workingDayId" AutoGenerateColumns="false" 
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Working Day" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            <ItemTemplate>
                                <asp:Label ID="lblworkingDayId" runat="server" Text='<%#Bind("workingDayId") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblworkingDay" runat="server" Text='<%#Bind("workingDay") %>' Visible="true"></asp:Label>
                                  <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label> 
                                 <asp:Label ID="lblbranchName" runat="server" Text='<%#Bind("branchName") %>' Visible="false"></asp:Label> 
                                 <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label> 
                                 <asp:Label ID="lblgymOwnerName" runat="server" Text='<%#Bind("gymOwnerName") %>' Visible="false"></asp:Label> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Time "  HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTime") %>' Visible="true"></asp:Label>-
                                   <asp:Label ID="lbltoTime" runat="server" Text='<%#Bind("toTime") %>' Visible="true"></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         
                           <asp:TemplateField HeaderText="isHoliday"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false"> 
                            <ItemTemplate>
                                <asp:Label ID="lblisHoliday" runat="server" Text='<%#Bind("isHoliday") %>' Visible="false"></asp:Label>      
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

