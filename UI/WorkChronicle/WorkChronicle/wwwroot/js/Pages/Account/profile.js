class Profile {
    constructor() {
    }

    Init() {
        const me = this;

        me.InitButtons();
        me.InitProfileInfo();
    }

    InitProfileInfo() {
        this.GetDataList(null, "GET", "/Account/GetUserById").then((response) => {
            if (response && response.success) {
                $("#first-name-readable").text(response.data.firstName);
                $("#first-name-writeable").val(response.data.firstName);

                $("#last-name-readable").text(response.data.lastName);
                $("#last-name-writeable").val(response.data.lastName);

                $("#email-readable").text(response.data.email);
                $("#email-writeable").val(response.data.email);

            }
            else if (response && !response.success) {
                alert("Cannot find user data!");
            }
        });
    }

    InitButtons() {
        const me = this;

        $("#save-profile-info").on("click", () => {
            $(".writeable").hide();
            $(".readable").show();
            $("#change-password-section").hide();
            $("#edit-profile-info").show();
            $("#save-profile-info").hide();
            $("#btn-back").hide();
            $("#change-password").hide();

            me.UpdateProfileInfo();
        });

        $("#change-password").on("click", () => {
            me.ChangePassword();
        });

        $("#btn-back").on("click", () => {
            $(".writeable").hide();
            $(".readable").show();
            $("#change-password-section").hide();
            $("#edit-profile-info").show();
            $("#save-profile-info").hide();
            $("#btn-back").hide();
            $("#change-password").hide();
        });

        $("#edit-profile-info").on("click", () => {
            $(".writeable").show();
            $(".readable").hide();
            $("#change-password-section").show();
            $("#save-profile-info").show();
            $("#btn-back").show();
            $("#change-password").show();
            $("#edit-profile-info").hide();
        });
    }

    UpdateProfileInfo() {
        const isValid = this.ValidateGeneralInfo();

        if (isValid) {
            const data = this.CollectGeneralInfoDataModel();

            this.GetDataList(data, "POST", "/Account/UpdateProfile").then((response) => {
                if (response && response.success) {
                    alert("Profile have been updated successfully!");

                    window.location.href = "/Account/Profile";
                }
                else if (response && !response.success) {
                    alert(response.errorMessage);
                }
            });
        }
    }

    ChangePassword() {
        const isValid = this.ValidatePasswordFields();

        if (isValid) {
            const data = this.CollectPasswordDataModel();

            this.GetDataList(data, "POST", "/Account/ChangePassword").then((response) => {
                if (response && response.success) {
                    alert("Password have been changed successfully!");

                    window.location.href = "/Account/Profile";
                }
                else if (response && !response.success) {
                    alert(response.errorMessage);
                }
            });
        }
    }

    CollectGeneralInfoDataModel() {
        const firstName = $("#first-name-writeable").val();
        const lastName = $("#last-name-writeable").val();
        const email = $("#email-writeable").val();


        const model = {
            FirstName: firstName,
            LastName: lastName,
            Email: email
        };

        return model;
    }

    CollectPasswordDataModel() {
        const oldPassword = $("#old-password").val();
        const newPassword = $("#new-password").val();
        const confirmPassword = $("#confirm-password").val();


        const model = {
            OldPassword: oldPassword,
            NewPassword: newPassword,
            ConfirmPassword: confirmPassword
        };

        return model;
    }

    ValidateGeneralInfo() {
        let isValid = true;

        const firstName = $("#first-name-writeable");
        const lastName = $("#last-name-writeable");
        const email = $("#email-writeable");
        

        if (!firstName.val()) {
            firstName.addClass("invalid-input");
            isValid = false;
        }

        if (!lastName.val()) {
            lastName.addClass("invalid-input");
            isValid = false;
        }

        if (!email.val()) {
            email.addClass("invalid-input");
            isValid = false;
        }

        return isValid;
    }

    ValidatePasswordFields() {
        let isValid = true;

        const oldPassword = $("#old-password");
        const newPassword = $("#new-password");
        const confirmPassword = $("#confirm-password");


        if (!oldPassword.val()) {
            oldPassword.addClass("invalid-input");
            isValid = false;
        }

        if (!newPassword.val()) {
            newPassword.addClass("invalid-input");
            isValid = false;
        }

        if (!confirmPassword.val()) {
            confirmPassword.addClass("invalid-input");
            isValid = false;
        }

        if (newPassword.val() !== confirmPassword.val()) {
            newPassword.addClass("invalid-input");
            confirmPassword.addClass("invalid-input");
            isValid = false;
        }

        return isValid;
    }

    RecheckNotNullFields(input) {
        if (!$(input).val() && !$(input).hasClass("invalid-input")) {
            $(input).addClass("invalid-input");
        }

        if ($(input).val()) {
            $(input).removeClass("invalid-input");
        }
    }

    RecheckPasswordFields() {
        if (!$("#new-password").val() && !$("#new-password").hasClass("invalid-input")) {
            $("#new-password").addClass("invalid-input");
        }

        if (!$("#confirm-password").val() && !$("#confirm-password").hasClass("invalid-input")) {
            $("#confirm-password").addClass("invalid-input");
        }

        if ($("#new-password").val() && $("#confirm-password").val()) {
            if ($("#new-password").val() === $("#confirm-password")) {
                $("#new-password").removeClass("invalid-input");
                $("#confirm-password").removeClass("invalid-input");
            }
            else {
                $("#new-password").addClass("invalid-input");
                $("#confirm-password").addClass("invalid-input");
            }
        }
    }

    // Get data
    GetDataList(data, type, url) {
        return $.ajax({
            type: type,
            url: url,
            data: data,
            success: function (response) {
                if (response.Success == false) {
                    alert(response.Message);
                }
            },
            error: function () {
                alert("Something went wrong during getting data");
            }
        });
    }
}