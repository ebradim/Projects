$("[data-form=login]").form({
  fields: {
    userName: ["notEmpty"],
    password: ["notEmpty"],
    persisted: [],
  },
  onSuccess: function (event, fields) {
    event.preventDefault();
    $.api({
      on: "now",
      action: "login",
      data: { ...fields, persisted: fields.persisted === "on" ? true : false },
      method: "POST",
      stateContext: this,
      onSuccess: function (response, _, _) {
        if (response.value == "/two-factor") {
          const _2faModal = $(".ui.modal#2fa_signin");
          const _2faSignInForm = $("form[data-form=2fa_signin]");
          const _2faSignInFormSubmit = _2faModal.find(".actions div.positive");
          _2faModal.modal("setting", "onApprove", function ($ele) {
            _2faSignInForm.form("validate form");
            return false;
          });
          _2faModal
            .modal("setting", "onShow", function ($ele) {
              _2faSignInForm.form("reset");
            })
            .modal("show");

          _2faSignInForm.form({
            fields: {
              code: ["empty", "integer"],
            },
            onSuccess: function (event, fields) {
              event?.preventDefault();
              $.api({
                on: "now",
                action: "2fa login",
                data: fields,
                method: "POST",
                stateContext: this,
                onSuccess: function (response, _, _) {
                  console.log(response);
                },
                onFailure: function (response, _) {
                  console.log(response);
                },
              });
            },
          });
        }
        window.location.href = "/home";
      },
      onFailure: function (response, _) {
        switch (response.status) {
          // sign in exception
          case 401: {
            if (response.title.match("PASSWORD_WRONG")) {
              $(this).form("add errors", { password: response.detail });
              $(this).form("add prompt", "password", response.detail);
              break;
            }
            // if (response.title.match("EMAIL_CONFIRMATION")) {
            //   $(this).form("add errors", { email: response.detail });
            //   $(this).form("add prompt", "email", response.detail);
            //   $.toast({
            //     class: "warning",
            //     title: response.actualError,
            //     message: `${response.title}, the confirmation link was sent to your inbox`,
            //   });
            //   break;
            // }
          }
          // locked
          case 423:
          // user not found
          case 404: {
            $(this).form("add errors", { userName: response.detail });
            $(this).form("add prompt", "userName", response.detail);
            break;
          }

          // validations
          case 400: {
            for (var identifier in response.errors) {
              $(this).form("add errors", {
                [identifier == "Credential" ? "userName" : identifier]:
                  response.errors[identifier],
              });
              $(this).form(
                "add prompt",
                identifier == "Credential" ? "userName" : identifier,
                response.errors[identifier]
              );
            }
            break;
          }
          // other
          default: {
            $.toast({
              class: "error",
              title: "Internal Server Error",
              message: `Failed to process this request, if this occures many times kindly contact support`,
            });
          }
        }
      },
    });
  },
});
