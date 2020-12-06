var contactManager = {
    application_hostname: getHost(),
    phonebookId: $('#phonebookId').val(),
    init: function () {
        // Declare a proxy to reference the hub. 
        var notificationHub = $.connection.phonebookSignalRHub;
        // Create a function that the hub can call to broadcast messages.
        notificationHub.client.showNotification = function (message) {
            alert(message);
            window.setTimeout(contactManager.refreshContactList(), 3000);
        };
    },
    refreshContactList() {
        location.reload();
    },
    
};
function getHost() {
    var virtualDirectory = window.location.pathname;
    var location = window.location.origin;

    if (virtualDirectory !== "" && virtualDirectory !== "/") {
        var firstDirectory = virtualDirectory.split("/");

        if (firstDirectory[1].startsWith("ELD")) {
            return location + "/" + firstDirectory[1];
        }
    }
    return location;
}
contactManager.init();