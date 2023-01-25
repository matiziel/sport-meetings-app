import { Button, TextInput, View, ScrollView } from 'react-native';
import Styles from '../../styles/Styles.js';
import DateTimePicker from '@react-native-community/datetimepicker';
import { useState, useContext, useEffect } from 'react';
import SportEventsService from '../../services/SportEventsService.js';

function CreateSportEventScreen({ navigation }) {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [limitOfParticipants, setLimitOfParticipants] = useState('');
    const [startDate, setStartDate] = useState(new Date());
    const [durationInHours, setDurationInHours] = useState('');
    const [location, setLocation] = useState('');

    const onStartDateChange = (event, selectedDate) => {
        if (event?.type === 'dismissed') {
            setStartDate(startDate);
            return;
        }
        setStartDate(selectedDate);
    };

    const createSportEvent = async (sportEvent) => {
        await SportEventsService.createSportEvent(sportEvent);
        navigation.navigate('SportEventsList', { updateListView: true });
        
    };

    return (
        <ScrollView>
            <View style={Styles.container}>
                <View style={Styles.inputView}>
                    <TextInput
                        style={Styles.TextInput}
                        placeholder="Name"
                        value={name}
                        onChangeText={setName}
                    />
                </View>
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
                        placeholder="Limit of participants"
                        keyboardType='numeric'
                        onChangeText={setLimitOfParticipants}
                        value={limitOfParticipants}
                        maxLength={3}
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

                <Button title="Create" onPress={() =>
                    createSportEvent({
                        name, description, limitOfParticipants, startDate, durationInHours, location
                    })
                } />

            </View>
        </ScrollView>
    );
}

export default CreateSportEventScreen;
