$(document).ready(function () {
  $(document).tooltip();
  $("#btnLogin").button({ icon: "ui-icon-key" });

  $("#txtUser").keypress(function (e) {
    const key = e.key;

    if (key === "Enter") login();
  });

  $("#txtPass").keypress(function (e) {
    const key = e.key;

    if (key === "Enter") login();
  });

  $("#btnLogin").click(function () { login(); });
});

function login() {
  $("#msgError").hide().text("");

  const userData = {
    user: $("#txtUser").val(),
    pass: $("#txtPass").val()
  };

  $.ajax({
    type: "POST",
    url: "Login.aspx/LoginProcess",
    data: JSON.stringify(userData),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      if (response.d.Success) {
        window.location.href = "/Paginas/Home.aspx";
        return;
      }

      $("#msgError")
        .text(response.d.Info)
        .stop(true, true)
        .fadeIn();
    },
    error: function (xhr, error) {
      $("#msgError")
        .text(`Error: ${xhr.status} - ${error}`)
        .stop(true, true)
        .fadeIn();
    }
  });
}
