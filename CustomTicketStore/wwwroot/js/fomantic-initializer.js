$(".ui.dropdown").dropdown();
$(".ui.checkbox").checkbox();
$(".ui.sidebar").sidebar("attach events", '.item.toggle', 'show');

//initialize all modals using data-modal and id
const modals = $(".ui.modal");
for (var i = 0; i < modals.length; i++) {
    const currentModal = $(modals[i]);
    const toggler = $(`div[data-modal=${currentModal.attr('id')}]`);
    if (toggler[0])
        currentModal.modal("setting", "blurring", true).modal("attach events", toggler)
}
const calendars = $(".ui.calendar");
for (var i = 0; i < modals.length; i++) {
    const currentCalendar = $(calendars[i]);
    currentCalendar.calendar({
        type: currentCalendar.attr("data-calendar"),
        onChange: function (date, text, mode) {

            const form = $(this).parentsUntil("form").parent();
            if (form) {
                form.form("validate field", $(this).find("input").attr("name"));
            }
        }
    })

}
$.fn.api.settings.api = {
    'login': '/api/accounts/login',
    '2fa login': '/api/accounts/login-2fa',
    'register': '/api/accounts/register',
    'external': '/api/accounts/complete',
    'change password': '/api/accounts/password',
    'change email': '/api/accounts/email-change',
    'read current user': '/api/users/.well-known',
    'update profile': '/api/users/profile',
};
$.fn.api.settings.beforeXHR = function (xhr) {
   
    xhr.setRequestHeader('X-Client-Type', 'ruba-B165');
    return xhr;
}
window.client = {
    secret:'rb:@EBA106D552B4:wc'
}
const notifyServerError = () => {
    $.toast({
        class: 'error',
        title: 'Internal Server Error',
        message: `Failed to process this request, if this occures many times kindly contact support`,
        className: {
            toast: 'ui message '
        },
        closeIcon: true,
        displayTime: 0,
    })
}

const notifyDataSaved = (message,title='Saved') => {
    $.toast({
        class: 'success',
        title: title,
        message: `${message}`,
        showIcon: true,
        showProgress: 'bottom'
    })
}
