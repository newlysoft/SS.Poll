﻿<%@ Page Language="C#" Inherits="SS.Poll.Pages.PageSettings" %>
  <%@ Register TagPrefix="ctrl" Namespace="SS.Poll.Controls" Assembly="SS.Poll" %>
    <!DOCTYPE html>
    <html>

    <head>
      <meta charset="utf-8">
      <link href="assets/plugin-utils/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
      <link href="assets/plugin-utils/css/plugin-utils.css" rel="stylesheet" type="text/css" />
      <link href="assets/plugin-utils/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
      <link href="assets/plugin-utils/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    </head>

    <body>

      <form runat="server">

        <header id="topnav">
          <div class="navbar-custom">
            <div class="container">
              <div id="navigation">
                <ul class="navigation-menu">
                  <li class="has-submenu">
                    <a href="<%=UrlEditItems%>">
                      <i class="ion-compose"></i>
                      投票项管理
                    </a>
                  </li>
                  <li class="has-submenu">
                    <a href="<%=UrlEditFields%>">
                      <i class="ion-compose"></i>
                      提交字段管理
                    </a>
                  </li>
                  <li class="has-submenu active">
                    <a href="javascript:;">
                      <i class="ion-compose"></i>
                      投票选项
                    </a>
                  </li>
                  <li class="has-submenu">
                    <a href="<%=UrlReturn%>">
                      <i class="ion-ios-undo"></i>
                      返回列表
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </header>


        <!-- container start -->
        <div class="container" style="margin-top: 70px;">
          <div class="m-b-25"></div>
          <div class="row">
            <div class="col-sm-12">
              <div class="card-box">
                <h4 class="text-dark  header-title m-t-0">投票选项设置</h4>
                <p class="text-muted m-b-25 font-13">在此设置投票以及选项信息</p>
                <asp:Literal id="LtlMessage" runat="server" />

                <div class="row">
                  <div class="form-horizontal" role="form">
                    <div class="form-group">
                      <label class="col-md-2 control-label">投票选项</label>
                      <div class="col-md-10">
                        <div class="checkbox checkbox-primary">
                          <asp:CheckBox ID="CbIsImage" text="包含图片" runat="server"></asp:CheckBox>
                          <asp:CheckBox ID="CbIsUrl" style="margin-left: 30px;" text="包含链接" runat="server"></asp:CheckBox>
                          <asp:CheckBox ID="CbIsTimeout" OnCheckedChanged="CbIsTimeout_CheckedChanged" AutoPostBack="true" style="margin-left: 30px;" text="限制时间段" runat="server"></asp:CheckBox>
                          <asp:CheckBox ID="CbIsCheckbox" OnCheckedChanged="CbIsCheckbox_CheckedChanged" AutoPostBack="true" style="margin-left: 30px;" text="可多选" runat="server"></asp:CheckBox>
                        </div>
                      </div>
                    </div>
                    <asp:PlaceHolder id="PhTimeout" runat="server">

                      <div class="form-group">
                        <label class="col-md-2 control-label" for="maxNum">开始时间</label>
                        <div class="col-md-3">
                          <ctrl:DateTimeTextBox ID="TbTimeToStart" class="form-control" Columns="40" runat="server"></ctrl:DateTimeTextBox>
                          <asp:RequiredFieldValidator ControlToValidate="TbTimeToStart" errorMessage=" *" foreColor="red" display="Dynamic" runat="server"
                          />
                        </div>
                        <span class="col-md-7">&nbsp;</span>
                      </div>
                      <div class="form-group">
                        <label class="col-md-2 control-label" for="maxNum">结束时间</label>
                        <div class="col-md-3">
                          <ctrl:DateTimeTextBox ID="TbTimeToEnd" class="form-control" Columns="40" runat="server"></ctrl:DateTimeTextBox>
                          <asp:RequiredFieldValidator ControlToValidate="TbTimeToEnd" errorMessage=" *" foreColor="red" display="Dynamic" runat="server"
                          />
                        </div>
                        <span class="col-md-7">&nbsp;</span>
                      </div>

                    </asp:PlaceHolder>
                    <asp:PlaceHolder id="PhCheckbox" runat="server">

                      <div class="form-group">
                        <label class="col-md-2 control-label" for="maxNum">最少选择</label>
                        <div class="col-md-1">
                          <asp:TextBox ID="TbCheckboxMin" class="form-control" Columns="40" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ControlToValidate="TbCheckboxMin" errorMessage=" *" foreColor="red" display="Dynamic" runat="server"
                          />
                        </div>
                        <label class="col-md-1 control-label" for="checkboxMax">最多选择</label>
                        <div class="col-md-1">
                          <asp:TextBox ID="TbCheckboxMax" class="form-control" Columns="40" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ControlToValidate="TbCheckboxMax" errorMessage=" *" foreColor="red" display="Dynamic" runat="server"
                          />
                        </div>
                        <span class="col-md-4">&nbsp;</span>
                      </div>

                    </asp:PlaceHolder>
                    <div class="form-group">
                      <label class="col-md-2 control-label">投票界面选项</label>
                      <div class="col-md-10">
                        <div class="checkbox checkbox-primary">
                          <asp:CheckBox ID="CbIsProfile" text="需要提交投票者信息" runat="server"></asp:CheckBox>
                          <asp:CheckBox ID="CbIsResult" style="margin-left: 30px;" text="投票结束后显示结果" runat="server"></asp:CheckBox>
                        </div>
                      </div>
                    </div>
                    <div class="m-b-25"></div>
                    <div class="form-group m-b-0">
                      <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button class="btn btn-success" onclick="BtnSubmit_Click" Text="保 存" runat="server" />
                      </div>
                    </div>
                  </div>
                </div>


              </div>
            </div>
          </div>
        </div>
        <!-- container end -->

        <asp:Literal id="LtlScript" runat="server"></asp:Literal>
      </form>
    </body>

    </html>
    <script src="assets/plugin-utils/js/jquery.min.js"></script>
    <script src="assets/plugin-utils/js/bootstrap.min.js"></script>