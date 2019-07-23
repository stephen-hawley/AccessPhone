# Access Phone Specification

# Overarching Goals
- App should improve quality of life
- App should be easily configurable/expandable
- App should be simple
- App should be accessible for people with cognitive impairments
- App should be accessible for people with physical impairments
- App should hide OS and other distractions


# Design Guidelines
- Use simple iconography
- Use uncluttered layouts
- When possible, use single, short words on controls
- Pair icons with words
- Optimize for one-hand operation

NB: Consider than any operation that is available may be put into three categories of permission: Available, Disallowed, and UnavailableNow. These correspond to “Yes, you can do this”, “You don’t get to see this”, and “Yes, you can do this, just not right now”. For example, the user might have the “Buy” module disallowed until she is better at understanding money so it wouldn’t even be present. Another user might have all phone calls shut off between 9PM and 7AM.


# Proposed Modules
- People - shows contacts, recently used contacts, allows messaging/calling
- Emergency - contacts emergency services
- Lock - manages NFC or IOT locking and unlocking
- Buy - manages a budget and allows payment
- To Do - manages a list of items with check boxes
- Camera - takes and shares photos
- Calendar - manages schedule
- Go - manages bus/train schedules
- Inbox - manages incoming messages


# People

Using the existing contact list on the phone, provide a read-only view of the contacts with images and names, sorted. Tapping a contact provides a larger view of the contact with available actions as buttons over the picture. Actions may include Talk and Call if permitted or applicable. For example, Call may be disallowed if the contact has no phone number. Talk may be disallowed if the contact has neither email nor SMS/MMS messaging.

Tapping on Call will initiate a call to the contact.
Tapping on Talk will initiate a messaging session to the contact.

# Talk

The talk module is an activity that is initiated by the People module.
Talk will operate in one of 4 modes based on its configuration:


1. Texting unassisted. User can type in a message with built-in word/spelling assistance. There will be a Send button, a Cancel button a Read, and an Attach button.
2. Speech to text. User is prompted speak a phrase and the speech will be converted to text. There will be a Send button, a Cancel button, a Read button and an Attach button. How should speech recognition be initiated? Automatic? Stopping at long dead spots?
3. Texting assisted/scripted. User is presented with a series of broad topics. Broad topics narrow to specific sub-topics. May narrow further. User selects a conversation and gets asked 0 or more questions to provide specifics. The final phrase gets shown in a text editor with a Send button, a Cancel button, a Read button and an Attach button.
4. Speech recorded. User is prompted to record a message. After recording, there will the following buttons, Send, Play, Redo, Attach and Cancel.

NB: Consider speech recorded assisted/scripted.

Example: Alice wants to spend time with Tammy. She taps on People and finds Tammy and taps on Talk. Talk is set for Texting assisted. She is presented with a short list of topics:

- Work
- Fun
- Need

She taps on Fun and sees the list:

- Movie
- Date
- Dance
- Food

She taps on Movie and is presented with a list of movies playing near her and prompted to select one. She picks one. She is presented with a list of days and times. She picks one. Then an editor window is filled with the text:

    Hi Tammy! Would you like to go see {MOVIE} with me on {DAY} at {TIME}?
    Alice

Alice taps Read to have it read back to her. Alice taps Send to send the message. Talk exits, returning to People.

Send will send the message.
Play will play back a recorded message.
Read will read the text of a message.
Attach will bring up a gallery of pictures and attach them to the message being sent.
Cancel will return exit Talk, returning to People.

# Camera

The camera app will be a simplified camera application that can take a photo (or a movie?) and save it. It should consist of a live view of the camera with three buttons over it: a photo button and a button to swap to front camera (if available) and a Back button. Should pinch zoom be supported? That doesn’t work well with one hand. Maybe a thumb slider on one side. There should be an optional audio cue when

# Lock

When in the Lock action is running, there should be a display that varies depending on the lock technology. If it’s a Near Field Communications (NFC) lock, there should be an animated scanner indicator with directions (“Bring phone near lock” or some such). When the NFC transaction is complete, the display will change and the lock is toggled. If the lock is an Internet of Things (IOT) lock, there should be a button that  is presented to lock/unlock the lock. Consider an option for IOT that checks location of the device relative to the lock and only makes it available when it is “close enough” or when the phone is on the same local area network as the lock.

# Emergency

The user is presented with a button to call emergency services. The button has a label over it that reads “Press and HOLD to call for help.” On pressing the button, an indicator (progress bar?) fills up over a span of 5-7 seconds, provides and indication (sound, haptic) and calls through the device’s phone.
Consider a short quiz before present the above screen:
Is there:

- A Fire
- Blood
- Unsafe Person
- No emergency
# To Do

The user is presented with a list of things that they need to do with check boxes next to each. The list could be entered locally (?), or read from a server or configuration. When the user taps on the check box, there is an indication that the action was done (sound, animation?) and either the item stays in place or gets moved to the bottom of the list. A checked out item can be unchecked and will return to the top of the list.

Configuration would include a list of tasks that would include a frequency (every/every other) and a time unit (Day, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Week Day, MWF, TuTh, SaSu, First Week of the Month, Second Week of the Month, Third Week of the Month, Fourth Week of the Month, Quarter).

For example, “Get Blood Test at Doctor’s Office” set with a frequency of Second Week would get added to the list on the second Sunday of the month. Maybe read from calendar with a specific notation on it (Ex: TODO:text of todo item)

# Calendar

TBD - general idea is simplified calendar maintenance. Calendars in general are complicated beasts. I don’t know how to present this to a user with cognitive difficulties.

# Go

TBD - Maybe look at phone location and infer nearest public transport and schedule. Ask where the user wants to go and set up a mechanism for it. Can this use the Google maps API? This seem like a problem that they have solved to a certain degree.

# Buy

TBD - The user is presented with a weekly/monthly budget with broad categories that may have limits on them. By using Apple Pay or Android Pay, can we read the item list and warn/deny some categories of item to be purchased (eg, a limit of 1 movie per month without permission or $x in groceries per week).

# Inbox

Shows a list of incoming messages in order of receipt for the active folder. Conceptually, there will be folders for “Newer”, “Older”, and “Trash”. New messages should be messages received in the past x days.  Older should be messages older than x days. Trash should be messages in the trash. Newer is default. Tapping on a folder label will show its contents. Trash will include a button to empty the trash with a warning. Tapping on a message shows the contents of the message. There should be buttons to show messages actions: Read, Reply, Back, Trash.

