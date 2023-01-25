import { useState, useContext, useEffect } from 'react';
import { Button, Text, TextInput, TouchableWithoutFeedback, View, Switch } from 'react-native';
import AuthContext from '../../context/AuthContext.js'
import TestService from '../../services/TestService.js';
import UsersService from '../../services/UsersService.js';
import SportEventsService from '../../services/SportEventsService.js';
import Styles from '../../styles/Styles.js';
import { FlatList } from 'react-native-gesture-handler';


function UserScreen({ navigation }) {
    const { signOut } = useContext(AuthContext);
    const [user, setUser] = useState({ id: null });
    const [userCreatedEvents, setUserCreatedEvents] = useState({ elements: [] });
    const [userSignedUpEvents, setUserSignedUpEvents] = useState({ elements: [] });

    const [isEnabled, setIsEnabled] = useState(false);
    const toggleSwitch = () => setIsEnabled(previousState => !previousState);


    useEffect(() => {
        const fetchData = async () => {
            const result = await UsersService.getUserInfo();
            setUser(result);

            const createdEvents = await SportEventsService.getEventsOwnedByUser();
            setUserCreatedEvents(createdEvents);

            const signedUpEvents = await SportEventsService.getEventsWhichUserAttend();
            setUserSignedUpEvents(signedUpEvents);
        };
        fetchData();
    }, [user.id, isEnabled]);

    return (
        <View>
            {user && <Text style={Styles.eventInfoTextStyle}>Login: {user.login}</Text>}
            {user && <Text style={Styles.eventInfoTextStyle}>Email: {user.email}</Text>}
            <Button title="Sign out" onPress={signOut} />
            <Switch
                trackColor={{ false: '#767577', true: '#81b0ff' }}
                thumbColor={isEnabled ? '#f5dd4b' : '#f4f3f4'}
                ios_backgroundColor="#3e3e3e"
                onValueChange={toggleSwitch}
                value={isEnabled}
            />
            <Text style={Styles.eventInfoTextStyle}>{isEnabled ? "Events owned by user" : "Events which user attends"}</Text>
            <FlatList
                data={isEnabled ? userCreatedEvents : userSignedUpEvents}
                renderItem={({ item }) =>
                    <TouchableWithoutFeedback onPress={() => navigation.navigate('SportEventInfo', { sportEventId: item.id })}>
                        <View style={Styles.flatlistStyle}>
                            <Text style={Styles.flatlistTextStyle}>{item.name}</Text>
                        </View>
                    </TouchableWithoutFeedback>
                }
            />

        </View>
    );
}

export default UserScreen;