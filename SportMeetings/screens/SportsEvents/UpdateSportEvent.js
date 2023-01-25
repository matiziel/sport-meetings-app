import { Button, TextInput, View, ScrollView } from 'react-native';
import Styles from '../../styles/Styles.js';
import DateTimePicker from '@react-native-community/datetimepicker';
import { useState, useContext, useEffect } from 'react';
import SportEventsService from '../../services/SportEventsService.js';

function UpdateSportEventScreen({ route, navigation }) {
    const [id, setId] = useState(null);
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            const { sportEventId } = route.params;
            const result = await SportEventsService.getSportEvent(sportEventId);
            setId(result.id);
            setDescription(result.id);
            setLocation(result.location);
        };
        fetchData();
    }, [id]);

    const updateSportEvent = async (sportEvent) => {
        await SportEventsService.updateSportEvent(sportEvent);
        navigation.navigate('SportEventsList', { updateListView: true });
    }


    return (
        <ScrollView>
            <View style={Styles.container}>
                <View style={Styles.inputView}>
                    <TextInput
                        style={Styles.TextInput}
                        placeholder="Description"
                        value={description}
                        onChangeText={setDescription}
                    />
                </View>
                <View style={Styles.inputView}>
                    <TextInput
                        style={Styles.TextInput}
                        placeholder="Location"
                        value={location}
                        onChangeText={setLocation}
                    />
                </View>

                <Button title="Update" onPress={() =>
                    updateSportEvent({ id, description, location })
                } />

            </View>
        </ScrollView>
    );
}

export default UpdateSportEventScreen;
