<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SevenSuite.PruebaWebApp.Paginas.Login" ResponseEncoding="utf-8" %>

<!DOCTYPE html>
<html lang="es">
  <head>
    <title>Iniciar Sesión</title>
    <meta charset="utf-8" />

    <link
      rel="stylesheet"
      href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"
    />
    <link
      rel="stylesheet"
      href="/Assets/css/Login.css"
    />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script src="/Assets/js/Login.js"></script>
  </head>

  <body>
    <div class="login-page ui-widget">
      <div class="login-container ui-widget-content ui-corner-all">
        <h2 class="ui-widget-header ui-corner-all">Iniciar Sesión</h2>

        <div class="form-group">
          <label for="txtUser">Usuario:</label>
          <input
            type="text"
            id="txtUser"
            class="ui-widget ui-widget-content ui-corner-all"
            title="Ingrese su nombre de usuario"
            placeholder="Ej: admin"
          />
        </div>

        <div class="form-group">
          <label for="txtPass">Contraseña:</label>
          <input
            type="password"
            id="txtPass"
            class="ui-widget ui-widget-content ui-corner-all"
            title="Ingrese su contraseña"
            placeholder="******"
          />
        </div>

        <button type="button" id="btnLogin">Ingresar</button>

        <div
          id="msgError"
          class="ui-state-error ui-corner-all error-msg"
          role="alert"
        >
        </div>
      </div>
    </div>
  </body>
</html>
