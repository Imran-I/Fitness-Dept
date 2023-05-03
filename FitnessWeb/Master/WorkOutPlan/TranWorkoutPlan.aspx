<%@ Page Title="Work Out Plan Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true"
    CodeFile="TranWorkoutPlan.aspx.cs" Inherits="Master_WorkOutPlan_TranWorkoutPlan" %>

<asp:Content ID="CtOwnerMaster" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        .gvhead {
            background-color: #959595;
            padding: 0.4rem;
            color: white;
            box-shadow: rgba(0, 0, 0, 0.15) 0px 5px 15px 0px;
            border-radius: 1rem;
            font-size: 1.1rem;
        }

            .gvhead:hover {
                background-color: #959595 !important;
                padding: 0.4rem;
                color: white !important;
                box-shadow: rgba(0, 0, 0, 0.15) 0px 5px 15px 0px;
                border-radius: 1rem;
                font-size: 1.1rem;
            }

        .gvsubHead {
            margin-top: 1rem;
            margin-left: 1rem;
            font-size: 0.8rem !important;
        }

        .editcolor {
            color: black;
        }

        .gvsub {
            background-color: #e9e9e9;
            padding: 0.4rem;
            border-radius: 1rem;
        }

        input[type="checkbox"] {
            margin-right: 0.4rem;
            margin-left: 0.1rem;
            margin-bottom: 20px;
        }

        .btnss {
            padding: 5px 50px;
            border: none;
            cursor: pointer;
            flex-grow: 1;
            z-index: 100;
            transition: all 0.5s;
            background-color: #fafafa;
        }

        .table > :not(:last-child) > :last-child > * {
            border-bottom: 0px;
        }

        tbody tr td span {
            white-space: normal;
        }

        .btnAddData {
            background-color: #e9e9e9;
            font-size: 1rem;
            /* border-radius: 1.2rem; */
            color: black;
            padding-left: 1rem;
            padding-right: 1rem;
            padding-top: 0.4rem;
            padding-bottom: 0.4rem;
        }
           .blink {
            animation: blink-animation 1s linear infinite;
            -webkit-animation: blink-animation 1s linear infinite;
        }

        @keyframes blink-animation {
            50% {
                opacity: 0;
            }
        }

        @-webkit-keyframes blink-animation {
            50% {
                opacity: 0;
            }
        }
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Diet Setup"></asp:Label>
            <i class="fafaicon">/</i>

            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Diet And Work Plan Assign"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">

        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Diet And Work Plan Assign</h5>
                  <div class="float-end">
                        <asp:LinkButton ID="btnApprove" runat="server"  Text="Click Here To Approve"  OnClick="btnApprove_Click"
                            CssClass="btnAdd" Style="background-color:white;color:black" Visible="false">
                         </asp:LinkButton>
                    </div>
            </div>
            <div class="justify-content-md-start" style="display: flex" id="divAddMasters" runat="server" visible="true">
                <div class="mr-3">
                    <asp:Button
                        ID="btnDietPlan"
                        runat="server"
                        Text="Diet Plan"
                        CausesValidation="false"
                        TabIndex="5"
                        OnClick="btnDietPlan_Click"
                        CssClass="btnss" />
                </div>
                <div class="mr-3">
                    <asp:Button
                        ID="btnWorkOutplan"
                        runat="server"
                        Text="Work Out Plan"
                        TabIndex="6"
                        OnClick="btnWorkOutplan_Click"
                        CausesValidation="false"
                        CssClass="btnss" />
                </div>

            </div>
            <div id="divDietPlan" runat="server">
                <div class="ddl">
                    <div class="row">
                        <div id="dietPlanForm" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 ">
                            <div class="row">
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtWakeUpTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">Wake Up Time<span class="reqiredstar">*</span></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfdUserName" ValidationGroup="TranDietOutPlan"
                                            ControlToValidate="txtWakeUpTime" runat="server" CssClass="rfvStyle"
                                            ErrorMessage="Enter Wake Up Time">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtFromDate" CssClass="txtbox fromDate" TabIndex="1" runat="server" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">From Date<span class="reqiredstar">*</span></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TranDietOutPlan"
                                            ControlToValidate="txtFromDate" runat="server" CssClass="rfvStyle"
                                            ErrorMessage="Enter From Date">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtToDate" CssClass="txtbox toDate" TabIndex="2" runat="server" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">To Date<span class="reqiredstar">*</span></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="TranDietOutPlan"
                                            ControlToValidate="txtToDate" runat="server" CssClass="rfvStyle"
                                            ErrorMessage="Enter To Date">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                    
                                    <asp:DropDownList ID="ddlDietType" runat="server" TabIndex="4" CssClass="form-select" RepeatDirection="Horizontal">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="TranDietOutPlan"
                                        ControlToValidate="ddlDietType" runat="server" CssClass="rfvStyle" InitialValue="0"
                                        ErrorMessage="Select Diet Type Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" >
                                    <div class="txtboxdiv">
                                        <asp:TextBox ID="txtTotalCalories" CssClass="txtbox" TabIndex="5" runat="server" placeholder=" " />
                                        <asp:Label CssClass="txtlabel" runat="server">Total Calories<span class="reqiredstar">*</span></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="TranDietOutPlan"
                                            ControlToValidate="txtTotalCalories" runat="server" CssClass="rfvStyle"
                                            ErrorMessage="EnterTotal Calories">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="margin-top: 1.5rem !important;">
                                    <asp:LinkButton ID="btnShowSubmitButton" Visible="false" Style="color: black" runat="server" Text="Show Button" OnClick="btnShowSubmitButton_Click">

                                    </asp:LinkButton>

                                </div>
                            </div>
                            <div class="float-end" id="DietSubmit" runat="server">
                                <asp:Button ID="btnSubmit" CssClass="btnSubmit" TabIndex="6" runat="server" Text="Submit" ValidationGroup="TranDietOutPlan" OnClick="btnSubmit_Click" />

                            </div>
                        </div>
                        <div id="divDietListView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12  table" visible="false">
                            <asp:DataList ID="gvBindBreakFast" runat="server" Width="60%" OnItemDataBound="gvBindBreakFast_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 " style="display: flex; font-size: 20px; color: black">
                                        <div class="col-8 col-sm-8 col-md-8 col-lg-8 col-xl-12 mb-2 ">
                                            <span><i class="fa fa-angle-down" aria-hidden="true"></i>&nbsp;&nbsp;</span>
                                            <asp:LinkButton ID="btnMealType" Style="color: black" runat="server" Text='<%#Bind("mealTypeName") %>' ></asp:LinkButton>

                                            <asp:DataList ID="dtlBreakfast" runat="server" BorderStyle="None" CssClass="gvsubHead" RepeatDirection="Vertical" Width="100%" Visible="true">
                                                <ItemTemplate>
                                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 gvsub" style="display: flex; justify-content: space-between; font-size: 20px; color: black">
                                                        <asp:Label ID="lblfoodItemId" runat="server" Text='<%#Bind("foodItemId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblmealType" runat="server" Text='<%#Bind("mealType") %>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblUserfoodDietTimeId" runat="server" Text='<%#Bind("UserfoodDietTimeId") %>' Visible="false"></asp:Label>
                                                        <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                                                            <h6 style="font-size: 0.8rem !important">
                                                                <asp:Label ID="lblfoodItemName" runat="server" Text='<%#Bind("foodItemName") %>' Visible="true"></asp:Label>
                                                                (
                                                                <asp:Label ID="Label4" runat="server" Style="font-size: 0.6rem !important" Text='<%#Eval("calories").ToString()+"cal" %>'
                                                                    Visible="true"></asp:Label>)
                                                            </h6>
                                                            <div style="font-size: 0.6rem !important">
                                                                <span>P : 
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("protein") %>' Visible="true"></asp:Label></span>&nbsp;
                                                                  <span>C : 
                                                                      <asp:Label ID="Label6" runat="server" Text='<%#Bind("carbs") %>' Visible="true"></asp:Label></span>&nbsp;
                                                                  <span>F : 
                                                                      <asp:Label ID="Label7" runat="server" Text='<%#Bind("fat") %>' Visible="true"></asp:Label></span>&nbsp;
                                                            </div>
                                                        </div>
                                                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                                            <asp:LinkButton ID="btnEdit" runat="server"
                                                                OnClick="btnEdit_Click"> <i  class="fa-solid fa-pencil fafaediticon editcolor"></i></asp:LinkButton>
                                                        </div>
                                                        <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">
                                                         <i class="fa fa-trash editcolor"></i></asp:LinkButton>
                                                        </div>
                                                        <asp:Label ID="lblprotein" runat="server" Text='<%#Bind("protein") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblcarbs" runat="server" Text='<%#Bind("carbs") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblfat" runat="server" Text='<%#Bind("fat") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblservingIn" runat="server" Text='<%#Bind("servingIn") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblservingInId" runat="server" Text='<%#Bind("servingInId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblcalories" runat="server" Text='<%#Bind("calories") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbldietTimeId" runat="server" Text='<%#Bind("foodDietTimeId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTime") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbltoTime" runat="server" Text='<%#Bind("toTime") %>' Visible="false"></asp:Label>


                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>

                                            <asp:Label ID="lblConfigId" runat="server" Text='<%#Bind("mealType") %>' Visible="false">  </asp:Label>
                                        </div>
                                        <div class="col-4 col-sm-4 col-md-4 col-lg-4 col-xl-12 mb-2 ">
                                            <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="btnAddData">
                                                 <i class="fa fa-plus AddPlus"></i> </asp:LinkButton>
                                        </div>

                                    </div>
                                </ItemTemplate>
                            </asp:DataList>

                      
                            <div class="float-end">
                              <%--  <asp:Button ID="btnApprove" CssClass="btnSubmit" TabIndex="8" runat="server" Text="Approve" OnClick="btnApprove_Click" />--%>
                              <%--  <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="7" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
            <div id="divWorkOutPlan" runat="server">
                <div id="WorkOutPlanForm" runat="server"> 
                <div class="ddl">
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                           
                            <asp:DropDownList ID="ddlWorkOutType" runat="server" TabIndex="1" CssClass="form-select" RepeatDirection="Horizontal" OnSelectedIndexChanged="ddlWorkOutType_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="TranWorkOutPlan"
                                ControlToValidate="ddlWorkOutType" runat="server" CssClass="rfvStyle" InitialValue="0"
                                ErrorMessage="Select Work Out Type">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                          
                            <asp:DropDownList ID="ddlWorkingDay" TabIndex="2" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Day *</asp:ListItem>
                                <asp:ListItem Value="1">Sunday</asp:ListItem>
                                <asp:ListItem Value="2">Monday</asp:ListItem>
                                <asp:ListItem Value="3">Tuesday</asp:ListItem>
                                <asp:ListItem Value="4">Wednesday</asp:ListItem>
                                <asp:ListItem Value="5">Thursday</asp:ListItem>
                                <asp:ListItem Value="6">Friday</asp:ListItem>
                                <asp:ListItem Value="7">Saturday</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="TranWorkOutPlan"
                                ControlToValidate="ddlWorkingDay" runat="server" CssClass="rfvStyle" InitialValue="0"
                                ErrorMessage="Select Day">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 " >
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtWorKFromdate" TabIndex="4" CssClass="txtbox fromDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">From Date<span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="TranWorkOutPlan"
                                    ControlToValidate="txtWorKFromdate" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter From Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3" >
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtWorkTodate" TabIndex="5" CssClass="txtbox toDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">To Date<span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="TranWorkOutPlan"
                                    ControlToValidate="txtWorkTodate" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter To Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divsubCategory" runat="server" visible="false">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:Label ID="Label1" runat="server" Text="Work Outs" Style="font-size: 15px; font-weight: 700"></asp:Label>
                            <asp:CheckBoxList ID="chkSubCategory" TabIndex="6" runat="server"
                                RepeatDirection="Vertical">
                            </asp:CheckBoxList>

                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:Label ID="Label2" runat="server" Text="Sets" Style="font-size: 15px; font-weight: 700"></asp:Label>
                             <asp:Label ID="Label8" runat="server" Text="Reps" Style="font-size: 15px; font-weight: 700;padding-left: 67px;"></asp:Label>
                             <asp:Label ID="Label9" runat="server" Text="Weights" Style="font-size: 15px; font-weight: 700;padding-left: 28px;"></asp:Label>
                            <div id="ddlBindSets" runat="server"></div>
                        </div>

                    </div>
                </div>
                <div class="float-end">
                    <asp:Button ID="btnWorkOutSubmit" CssClass="btnSubmit" TabIndex="7" runat="server" Text="Submit" ValidationGroup="TranWorkOutPlan" OnClick="btnWorkOutSubmit_Click" />
                    <asp:Button ID="btnWorkOutCancel" CssClass="btnCancel" TabIndex="8" runat="server" Text="Cancel" OnClick="btnWorkOutCancel_Click" />
                </div>
                   </div>
                <br />
                <div id="DivGridView" runat="server" style="margin-top:30px" >
                     <asp:GridView ID="gvUserWorkOutPlan" runat="server" AutoGenerateColumns="false"
                         CssClass="table table-striped table-hover border display gvFilter" style="font-size: 0.8rem">
                    <Columns>
                        <asp:TemplateField HeaderText="S.no"  HeaderStyle-CssClass="GvHead">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Category Name"  HeaderStyle-CssClass="GvHead" >
                            <ItemTemplate>
                                <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgymOwnerId"   runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false" > </asp:Label>
                                <asp:Label ID="lblgymOwnerName"   runat="server" Text='<%#Bind("gymOwnerName") %>' Visible="false" > </asp:Label>
                                <asp:Label ID="lblbranchId"   runat="server" Text='<%#Bind("branchId") %>'  Visible="false"> </asp:Label>
                                <asp:Label ID="lblbranchName"   runat="server" Text='<%#Bind("branchName") %>' Visible="false" > </asp:Label>
                                <asp:Label ID="lblworkoutPlanId"   runat="server" Text='<%#Bind("workoutPlanId") %>' Visible="false" > </asp:Label>
                                <asp:Label ID="lblworkoutCatTypeId"   runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblworkoutCatTypeName"   runat="server" Text='<%#Bind("workoutCatTypeName") %>' > </asp:Label>
                                <asp:Label ID="lblworkoutTypeId"  runat="server" Text='<%#Bind("workoutTypeId") %>' Visible="false" > </asp:Label>
                              
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type Name"  HeaderStyle-CssClass="GvHead" >
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutTypeName"   runat="server" Text='<%#Bind("workoutTypeName") %>' > </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date"  HeaderStyle-CssClass="GvHead" Visible="false" >
                            <ItemTemplate>
                               <asp:Label ID="lblfromDate"   runat="server" Text='<%#Bind("fromDate") %>' > </asp:Label>-
                                <asp:Label ID="lbltoDate" runat="server" Text='<%#Bind("toDate") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Day" HeaderStyle-CssClass="GvHead">
                            <ItemTemplate>
                                <asp:Label ID="lblday" runat="server" Text='<%#Bind("day") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Set Type"  HeaderStyle-CssClass="GvHead"> 
                            <ItemTemplate>
                                <asp:Label ID="lblcsetType"   runat="server" Text='<%#Bind("csetType") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblsetTypeName" runat="server" Text='<%#Bind("setTypeName") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.Of Reps"  HeaderStyle-CssClass="GvHead"> 
                            <ItemTemplate>
                              <asp:Label ID="cnoOfReps" runat="server" Text='<%#Bind("cnoOfReps") %>' > </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight"  HeaderStyle-CssClass="GvHead"> 
                            <ItemTemplate>
                             <asp:Label ID="lblcweight" runat="server" Text='<%#Bind("cweight") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                         </Columns>
                </asp:GridView>
                </div>
            </div>

        </div>

        <div id="AddBenefits" runat="server" class="DisplyCard" visible="false">
            <div class="DisplyCardPostion table-responsive">
                <div class="PageHeader" style="margin-top: -25px">
                    <h5>Food Item <span>List</span>
                        <a onclick="btnClose()" class="float-end btnclose">
                            <i class="fa-solid fa-x"></i></a>
                    </h5>

                </div>
                <div class="row" style="margin-top: -20px">
                    <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                        <asp:Label ID="lblFoodItem" runat="server" Text="Food Item" Style="font-size: 10px; margin-left: -279px; font-weight: 700"></asp:Label>
                        <asp:DropDownList ID="ddlFoodItemList" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="FoodItem"
                            ControlToValidate="ddlFoodItemList" runat="server" CssClass="rfvStyle" InitialValue="Select"
                            ErrorMessage="Select Food Item">
                        </asp:RequiredFieldValidator>
                    </div>


                </div>
                <div class="text-end">
                    <asp:Button ID="btnSubSubmit" CssClass="btnSubmit" ValidationGroup="FoodItem" OnClick="btnSubSubmit_Click" runat="server" Text="Submit" />
                    <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">

        function btnClose() {
            $('#<%= AddBenefits.ClientID %>').css("display", "none");
        }
        function myFunction() {
            var x = document.getElementById("btnShowSubmitButton");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>

</asp:Content>

