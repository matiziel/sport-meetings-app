import ApiClient from "./ApiClient.js";
import AuthService from "./AuthService.js";


const SignUpsService = {
    isUserSignedUpForEvent: async (sportEventId) => {
        try {
            var result = await ApiClient.get('SignUps/IsUserSignUp/' + sportEventId, await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
        }
    },

    signUpForEvent: async (sportEventId) => {
        try {
            await ApiClient.post('SignUps/' + sportEventId, {}, await AuthService.getAuthHeader());
        }
        catch (error) {
            console.log(error);
        }
    },

    signOutFromEvent: async (sportEventId) => {
        try {
            await ApiClient.delete('SignUps/' + sportEventId, await AuthService.getAuthHeader());
        }
        catch (error) {
            console.log(error);
        }
    }
};

export default SignUpsService;