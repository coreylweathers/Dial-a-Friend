window.appFunctions = {
    setup: function (token) {
        console.log('Getting connected');

        // Setup Twilio Device
        Twilio.Device.setup(token);

        Twilio.Device.ready(() => {
            console.log('We are connected and ready to do the thing');
        });

        Twilio.Device.error((err) => {
            console.error('This should not have been reached. We need to do something here');
            console.error(err);
        });
    },
    placeCall: function (destination) {
        console.log(`Calling ${destination}`);
        Twilio.Device.connect({ phone: destination });
        console.log(`Successfully called ${destination}`);
    },
    endCall: function () {
        console.log('Ending the call');
        Twilio.Device.disconnectAll();
        console.log('Successfully ended the call');
    }
};