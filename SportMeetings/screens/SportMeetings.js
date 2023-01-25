import { createStackNavigator } from '@react-navigation/stack';
import CreateSportEventScreen from './SportsEvents/CreateSportEvent';
import SportEventsListScreen from './SportsEvents/SportEventsList';
import UserScreen from './User/User';
import SportEventInfoScreen from './SportsEvents/SportEventInfo';
import UpdateSportEventScreen from './SportsEvents/UpdateSportEvent';


const Stack = createStackNavigator();

function SportMeetings() {
    return (
        <Stack.Navigator initialRouteName="SportEventsList">
            <Stack.Screen name="SportEventsList" component={SportEventsListScreen} initialParams={{ updateListView: false }} />
            <Stack.Screen name="SportEventInfo" component={SportEventInfoScreen} />
            <Stack.Screen name="CreateSportEvent" component={CreateSportEventScreen} />
            <Stack.Screen name="UpdateSportEvent" component={UpdateSportEventScreen} />

            <Stack.Screen name="PersonalData" component={UserScreen} />
        </Stack.Navigator>
    );
}

export default SportMeetings;
