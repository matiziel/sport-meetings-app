import ApiClient from "./ApiClient.js";
import AuthService from "./AuthService.js";


const UsersService = {
    getUserInfo: async (sportEventId) => {
        try {
            var result = await ApiClient.get('Users', await AuthService.getAuthHeader());
            return result.data;
        }
        catch (error) {
            console.log(error);
            return null;
        }
    },

};

export default UsersService;