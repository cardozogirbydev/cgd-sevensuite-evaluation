var idClientToDelete = 0;

$(document).ready(function () {
  $(document).tooltip({
    position: { my: "center bottom", at: "center top" }
  });

  $("#btnFilter").button({ icon: "ui-icon-search" });
  $("#btnPrintPDF").button({ icon: "ui-icon-print" });
  $("#btnNewClient").button({ icon: "ui-icon-plus" });
  $("#lnkLogout").addClass("ui-button ui-widget ui-corner-all");
  $(".filters-actions").controlgroup();
  $("#txtFechaNac").datepicker({
    dateFormat: "dd/mm/yy",
    changeMonth: true,
    changeYear: true,
    yearRange: "-100:+0"
  });

  $("#modalClient").dialog({
    autoOpen: false,
    modal: true,
    width: 400,
    buttons: {
      "Guardar": function () {
        const idEstadoCivil = parseInt($("#slEstadoCivil").val(), 10);
        const client = {
          id_clie: parseInt($("#txtIdClie").val(), 10),
          cedula: $("#txtCedula").val().trim(),
          nombre: $("#txtNombre").val().trim(),
          genero: $("#slGenero").val(),
          fecha_nac: $("#txtFechaNac").val(),
          id_estado_civil: idEstadoCivil
        };

        if (!client.cedula || !client.nombre || !client.fecha_nac || Number.isNaN(idEstadoCivil)) {
          showInfoDialog("Complete todos los campos obligatorios antes de guardar.", true, "Validacion");
          return;
        }

        $.ajax({
          type: "POST",
          url: "/Paginas/Home.aspx/SaveClient",
          data: JSON.stringify({ clientData: client }),
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (response) {
            if (response.d.Success) {
              $("#modalClient").dialog("close");
              loadClients();
              showInfoDialog("El cliente ha sido guardado satisfactoriamente.");
              return;
            }

            showInfoDialog(`Error: ${response.d.Info}`, true, "Error");
          },
          error: function (xhr, error) {
            showInfoDialog(`Error guardando cliente: ${xhr.status} - ${error}`, true, "Error");
          }
        });
      },
      "Cancelar": function () { $(this).dialog("close"); }
    }
  });

  $("#slGenero").selectmenu({ width: 250, appendTo: "#modalClient" });
  $("#slEstadoCivil").selectmenu({ width: 250, appendTo: "#modalClient" });

  $("#modalConfirm").dialog({
    autoOpen: false,
    resizable: false,
    height: "auto",
    width: 400,
    modal: true,
    buttons: {
      "Eliminar": function () {
        deleteClient(idClientToDelete);
        $(this).dialog("close");
      },
      "Cancelar": function () { $(this).dialog("close"); }
    }
  });

  $("#modalInfo").dialog({
    autoOpen: false,
    modal: true,
    width: 420,
    buttons: {
      "Aceptar": function () { $(this).dialog("close"); }
    }
  });

  loadClients();
  loadCivilStatuses();
  toggleClearFilterButton();

  $("#txtFilterName").keypress(function (e) {
    const key = e.key;

    if (key === "Enter") filterClients();
  });

  $("#txtFilterName").on("input", function () {
    toggleClearFilterButton();
  });

  $("#btnFilter").click(function () { filterClients(); });

  $("#btnClearFilterInline").click(function () {
    $("#txtFilterName").val("").focus();
    toggleClearFilterButton();
    loadClients();
  });

  $("#btnNewClient").click(function () {
    $("#formClient")[0].reset();
    $("#txtIdClie").val("0");
    $("#slGenero").val("M").selectmenu("refresh");

    if ($("#slEstadoCivil option").length > 0) {
      $("#slEstadoCivil").val($("#slEstadoCivil option:first").val()).selectmenu("refresh");
    }

    $("#modalClient").dialog("option", "title", "Nuevo Cliente").dialog("open");
  });

  $("#btnPrintPDF").click(function () {
    const filtro = $("#txtFilterName").val();

    window.open(`/Paginas/ReporteClientes.aspx?filtro=${encodeURIComponent(filtro)}`, "_blank");
  });
});

function filterClients() {
  const filter = $("#txtFilterName").val();
  loadClients(filter);
}

function toggleClearFilterButton() {
  const hasText = $.trim($("#txtFilterName").val()).length > 0;
  $("#btnClearFilterInline").toggle(hasText);
}

function showInfoDialog(message, isError = false, title = "Información") {
  const info = $("#modalInfoMessage");

  info
    .text(message)
    .removeClass("ui-state-highlight ui-state-error")
    .addClass(isError ? "ui-state-error" : "ui-state-highlight");

  $("#modalInfo")
    .dialog("option", "title", title)
    .dialog("open");
}

function loadClients(filtro = "") {
  $.ajax({
    type: "POST",
    url: "/Paginas/Home.aspx/GetClients",
    data: JSON.stringify({ filtro: filtro }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      if (!response.d.Success) {
        showInfoDialog(response.d.Info ?? "No se pudo consultar los clientes.", true, "Error");
        return;
      }

      const tbody = $("#tbClients tbody");
      tbody.empty();

      if (response.d.Data.length === 0) {
        tbody.append('<tr><td colspan="6" style="text-align:center;">No se encontraron registros</td></tr>');
        return;
      }

      $.each(response.d.Data, function (index, item) {
        const desc_estado_civil = item.desc_estado_civil.toLowerCase().replace(/\b\w/, s => s.toUpperCase());

        const fila = `
          <tr>
            <td>${item.cedula}</td>
            <td>${item.nombre}</td>
            <td>${item.genero == 'M' ? 'Masculino' : 'Femenino'}</td>
            <td>${item.fecha_nac}</td>
            <td>${desc_estado_civil}</td>
            <td class="fieldActions">
              <button
                type="button"
                title="Editar el cliente"
                onclick="editClient(${item.id_clie})"
                class="btn-edit js-action-edit"
              >
                Editar
              </button>

              <button
                type="button"
                title="Eliminar el cliente"
                onclick="deleteClientConfirm(${item.id_clie})"
                class="btn-delete js-action-delete"
              >
                Eliminar
              </button>
            </td>
          </tr>
        `;

        tbody.append(fila);
      });

      $(".js-action-edit").button({ icon: "ui-icon-pencil" });
      $(".js-action-delete").button({ icon: "ui-icon-trash" });
    },
    error: function (xhr, error) {
      showInfoDialog(`Error consultando clientes: ${xhr.status} - ${error}`, true, "Error");
    }
  });
}

function loadCivilStatuses() {
  $.ajax({
    type: "POST",
    url: "/Paginas/Home.aspx/GetCivilStatuses",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      if (!response.d.Success) {
        showInfoDialog(response.d.Info ?? "No se pudo cargar los estados civiles.", true, "Error");
        return;
      }

      const select = $("#slEstadoCivil");
      select.empty();

      if (!response.d.Data || response.d.Data.length === 0) {
        select.append($("<option>", { value: "", text: "No hay estados civiles" }));
        select.selectmenu("refresh");
        return;
      }

      $.each(response.d.Data, function (index, item) {
        select.append($("<option>", { value: item.id_estado_civil, text: item.descripcion }));
      });

      select.val(select.find("option:first").val());
      select.selectmenu("refresh");
    },
    error: function (xhr, error) {
      showInfoDialog(`Error cargando estados civiles: ${xhr.status} - ${error}`, true, "Error");
    }
  });
}

function editClient(id) {
  $.ajax({
    type: "POST",
    url: "/Paginas/Home.aspx/GetClientById",
    data: JSON.stringify({ id }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      if (!response.d.Success || !response.d.Data) {
        showInfoDialog(response.d.Info ?? "No se pudo obtener el cliente.", true, "Error");
        return;
      }

      const data = response.d.Data;

      $("#txtIdClie").val(data.id_clie);
      $("#txtCedula").val(data.cedula);
      $("#txtNombre").val(data.nombre);
      $("#slGenero").val(data.genero).selectmenu("refresh");
      $("#txtFechaNac").val(data.fecha_nac);
      $("#slEstadoCivil").val(String(data.id_estado_civil)).selectmenu("refresh");

      $("#modalClient").dialog("option", "title", "Editar Cliente").dialog("open");
    },
    error: function (xhr, error) {
      showInfoDialog(`Error obteniendo cliente: ${xhr.status} - ${error}`, true, "Error");
    }
  });
};

function deleteClientConfirm(id) {
  idClientToDelete = id;
  $("#modalConfirm").dialog("open");
};

function deleteClient(id) {
  $.ajax({
    type: "POST",
    url: "/Paginas/Home.aspx/DeleteClient",
    data: JSON.stringify({ id }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      if (response.d.Success) {
        showInfoDialog("Cliente eliminado satisfactoriamente.");
        loadClients();
        return;
      }

      showInfoDialog(`Error: ${response.d.Info}`, true, "Error");
    },
    error: function (xhr, error) {
      showInfoDialog(`Error eliminando cliente: ${xhr.status} - ${error}`, true, "Error");
    }
  });
}
