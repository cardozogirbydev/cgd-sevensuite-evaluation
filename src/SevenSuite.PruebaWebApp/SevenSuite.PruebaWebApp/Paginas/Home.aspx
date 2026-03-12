<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SevenSuite.PruebaWebApp.Paginas.Home" ResponseEncoding="utf-8" %>

<!DOCTYPE html>
<html lang="es">
  <head>
    <title>Home</title>
    <meta charset="utf-8" />

    <link
      rel="stylesheet"
      href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css"
    />
    <link
      href="/Assets/css/Home.css"
      rel="stylesheet"
    />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script src="/Assets/js/Home.js"></script>
  </head>

  <body class="ui-widget">
    <header class="ui-widget-header ui-corner-bottom">
      <h1>Sistema Seven Suite</h1>

      <div class="user-badge">
        <span>
          Bienvenido,
          <strong><%= UserName %></strong>
        </span>

        <a href="/Paginas/Home.aspx?action=logout" id="lnkLogout">Cerrar Sesion</a>
      </div>
    </header>

    <main>
      <section class="ui-widget-content ui-corner-all main-panel">
        <h2>Gestión de clientes</h2>

        <div class="filters ui-widget-content ui-corner-all">
          <div class="filter-input-wrap">
            <input
              type="text"
              id="txtFilterName"
              class="ui-widget ui-widget-content ui-corner-all"
              placeholder="Buscar por nombre o cedula..."
              title="Filtre los clientes por su nombre o cédula"
            />

            <button
              type="button"
              id="btnClearFilterInline"
              class="btn-clear-inline"
              title="Limpiar filtro"
              aria-label="Limpiar filtro"
            >
              <span class="ui-icon ui-icon-close"></span>
            </button>
          </div>

          <div class="filters-actions">
            <button type="button" id="btnFilter">Buscar</button>
            <button type="button" id="btnPrintPDF">Ver Reporte</button>
          </div>
        </div>

        <table id="tbClients" class="display-table ui-widget ui-widget-content ui-corner-all">
          <thead>
            <tr>
              <th>Cédula</th>
              <th>Nombre</th>
              <th>Género</th>
              <th>Fecha de nacimiento</th>
              <th>Estado civil</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody></tbody>
        </table>
      </section>
    </main>

    <div id="modalClient" title="Información del cliente">
      <form id="formClient">
        <input type="hidden" id="txtIdClie" value="0" />

        <div class="form-group">
          <label>Cédula:</label>
          <input type="text" id="txtCedula" class="ui-widget ui-widget-content ui-corner-all" title="Ingrese la cédula" />
        </div>

        <div class="form-group">
          <label>Nombre:</label>
          <input type="text" id="txtNombre" class="ui-widget ui-widget-content ui-corner-all" title="Ingrese el nombre completo" />
        </div>

        <div class="form-group">
          <label>Género:</label>
          <select id="slGenero" class="ui-widget ui-widget-content ui-corner-all" title="Seleccione el género">
            <option value="M">Masculino</option>
            <option value="F">Femenino</option>
          </select>
        </div>

        <div class="form-group">
          <label>Fecha de nacimiento</label>
          <input type="text" id="txtFechaNac" class="ui-widget ui-widget-content ui-corner-all" title="Seleccione su fecha de nacimiento" readonly />
        </div>

        <div class="form-group">
          <label>Estado civil</label>
          <select id="slEstadoCivil" class="ui-widget ui-widget-content ui-corner-all" title="Seleccione su estado civil actual"></select>
        </div>
      </form>
    </div>

    <div id="modalConfirm" title="¿Eliminar cliente?">
      <p>
        <span class="ui-icon ui-icon-alert"></span>
        Este cliente se eliminará de forma permanente. ¿Desea continuar?
      </p>
    </div>

    <div id="modalInfo" title="Información">
      <p id="modalInfoMessage" class="ui-state-highlight ui-corner-all"></p>
    </div>

    <div class="btnNewClient-content">
      <button type="button" id="btnNewClient">
        Nuevo cliente
      </button>
    </div>
  </body>
</html>
