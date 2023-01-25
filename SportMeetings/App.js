import * as React from 'react';
import { Button, Text, TextInput, View } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import AuthScreen from './screens/Authentication/Auth.js';
import { useMemo, useReducer, useEffect } from 'react';

import AuthContext from './context/AuthContext.js';
import AuthService from './services/AuthService.js';
import SportMeetings from './screens/SportMeetings.js';

function SplashScreen() {
  return (
    <View>
      <Text>Loading...</Text>
    </View>
  );
}

const Stack = createStackNavigator();

export default function App({ navigation }) {
  const [state, dispatch] = useReducer(
    (prevState, action) => {
      switch (action.type) {
        case 'RESTORE_TOKEN':
          return {
            ...prevState,
            userToken: action.token,
            isLoading: false,
          };
        case 'SIGN_IN':
          return {
            ...prevState,
            isSignout: false,
            userToken: action.token,
          };
        case 'SIGN_OUT':
          return {
            ...prevState,
            isSignout: true,
            userToken: null,
          };
      }
    },
    {
      isLoading: true,
      isSignout: false,
      userToken: null,
    }
  );

  useEffect(() => {
    // Fetch the token from storage then navigate to our appropriate place
    const bootstrapAsync = async () => {
      let userToken;

      try {
        // Restore token stored in `SecureStore` or any other encrypted storage
        // userToken = await SecureStore.getItemAsync('userToken');
      } catch (e) {
        // Restoring token failed
      }

      // After restoring token, we may need to validate it in production apps

      // This will switch to the App screen or Auth screen and this loading
      // screen will be unmounted and thrown away.
      dispatch({ type: 'RESTORE_TOKEN', token: null });
    };

    bootstrapAsync();
  }, []);

  const authContext = useMemo(
    () => ({
      signIn: async (data) => {
        await AuthService.signIn(data.username, data.password);

        dispatch({ type: 'SIGN_IN', token: 'token' });
      },
      signOut: async () => {
        await AuthService.signOut();
        dispatch({ type: 'SIGN_OUT' })
      },
      signUp: async (data) => {
        await AuthService.signUp(data.username, data.email, data.password);
        await AuthService.signIn(data.username, data.password);

        dispatch({ type: 'SIGN_IN', token: 'token' });
      }
    }),
    []
  );

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        <Stack.Navigator>
          {state.isLoading ? (
            <Stack.Screen name="Splash" component={SplashScreen} />
          ) : state.userToken == null ? (
            <Stack.Screen
              name="Auth"
              component={AuthScreen}
              options={{
                animationTypeForReplace: state.isSignout ? 'pop' : 'push'
              }}
            />
          ) : (
            <Stack.Screen name="SportMeetings" component={SportMeetings} />
          )}
        </Stack.Navigator>
      </NavigationContainer>
    </AuthContext.Provider>
  );
}