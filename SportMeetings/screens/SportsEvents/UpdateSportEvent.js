import { Button, TextInput, View, ScrollView } from 'react-native';
import Styles from '../../styles/Styles.js';
import DateTimePicker from '@react-native-community/datetimepicker';
import { useState, useContext, useEffect } from 'react';
import SportEventsService from '../../services/SportEventsService.js';

function UpdateSportEventScreen({ navigation }) {
    const [id, setId] = useState(null);
    const [description, setDescription] = useState('');
    const [startDate, setStartDate] = useState(new Date());
    const [durationInHours, setDurationInHours] = useState('');
    const [location, setLocation] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            const { sportEventId } = route.params;
            const result = await SportEventsService.getSportEvent(sportEventId);

            setId(result.id);
            setDescription(result.id);
            setStartDate(result.startDate);
            setDurationInHours(result.durationInHours);
            setLocation(result.location);
        };
        fetchData();
    }, [id]);

    const updateSportEvent = async (sportEvent) => {
        await SportEventsService.updateSportEvent(sportEvent);
        navigation.navigate('SportEventsList');
    }

    const onStartDateChange = (event, selectedDate) => {
        if (event?.type === 'dismissed') {
            setStartDate(startDate);
            return;
        }
        setStartDate(selectedDate);
    };

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
                        placeholder="Duration in hours"
                        keyboardType='numeric'
                        onChangeText={setDurationInHours}
                        value={durationInHours}
                        maxLength={1}
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
                <View>
                    <DateTimePicker mode="datetime" value={startDate} onChange={onStartDateChange} />
                </View>

                <Button title="Update" onPress={() =>
                    updateSportEvent({ description, startDate, durationInHours, location })
                } />

            </View>
        </ScrollView>
    );
}

export default UpdateSportEventScreen;
