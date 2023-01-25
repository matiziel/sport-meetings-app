import ApiClient from "./ApiClient.js";
import AuthService from "./AuthService.js";


const SportEventsService = {
    getSportEvents: async () => {
        try {
            var result = await ApiClient.get('SportEvents', await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
        }
    },

    getSportEvent: async (sportEventId) => {
        try {
            var result = await ApiClient.get('SportEvents/' + sportEventId, await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
        }
    },

    isUserEventOwner: async (sportEventId) => {
        try {
            var result = await ApiClient.get('SportEvents/IsUserEventOwner/' + sportEventId, await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
        }
    },

    createSportEvent: async (sportEvent) => {
        try {
            var result = await ApiClient.post('SportEvents', sportEvent, await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
            return null;
        }
    },

    updateSportEvent: async (sportEvent) => {
        try {
            var result = await ApiClient.put('SportEvents', sportEvent, await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
            return null;
        }
    }

};

export default SportEventsService;