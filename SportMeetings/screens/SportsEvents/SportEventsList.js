import { useState, useContext, useEffect } from 'react';
import { Button, Text, TextInput, View, TouchableWithoutFeedback } from 'react-native';
import AuthContext from '../../context/AuthContext.js';
import Styles from '../../styles/Styles.js';
import SportEventsService from '../../services/SportEventsService.js';
import { FlatList } from 'react-native-gesture-handler';

function SportEventsListScreen({ navigation }) {
    const [sportEvents, setSportEvents] = useState({ elements: [] });

    useEffect(() => {
        const fetchData = async () => {
            const result = await SportEventsService.getSportEvents();
            setSportEvents(result);
        };
        fetchData();
    }, []);

    return (
        <View style={Styles.container}>
            <Button title="Create event" onPress={() => navigation.navigate('CreateSportEvent')} />
            <Button title="PersonalData" onPress={() => navigation.navigate('PersonalData')} />
            <FlatList
                data={sportEvents}
                renderItem={({ item }) =>
                    <TouchableWithoutFeedback onPress={() => navigation.navigate('SportEventInfo', { sportEventId: item.id })}>
                        <View style={Styles.flatlistStyle}>
                            <Text style={Styles.flatlistTextStyle}>{item.name}</Text>
                        </View>
                    </TouchableWithoutFeedback>
                }
            />
        </View>);
}

export default SportEventsListScreen;