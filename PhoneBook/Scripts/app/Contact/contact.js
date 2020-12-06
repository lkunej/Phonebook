var contactManager = {

    init: function () {
        // Declare a proxy to reference the hub. 
        var notificationHub = $.connection.phonebookSignalRHub;
        // Create a function that the hub can call to broadcast messages.
        notificationHub.client.showNotification = function (message) {
            alert(message);
            this.refreshContactList();
        };
    }

    refreshContactList() {

    }
}
contactManager.init();