var appManager = {

    init: function () {
        this.startSignalR();
    },
    startSignalR: function () {

        // Start the connection.
        $.connection.hub.start().done();
    }
}
appManager.init();
