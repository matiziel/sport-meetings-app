import ApiClient from "./ApiClient.js";
import AuthService from "./AuthService.js";


const SportEventsService = {
    getSportEvents: async () => {
        try {
            var result = await ApiClient.get('SportEvents', await AuthService.getAuthHeader());
            console.log(result.data);
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
    }

};

export default SportEventsService;