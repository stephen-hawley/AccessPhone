# HelpMe
HelpMe is an activity that allows a user to seek help when they are in an emergency.
We need to assume that in the event of an emergency, a user may be anxious and it may be hard for them to think clearly.
One way to manage this is to break up emergency situations to help guide them to an appropriate action.
Here are some examples:

    - Are you hurt?
        - Yes
        - No
    - Are you sick?
        - Yes
        - No
    - Are you lost?
        - Yes
        - No
    - Are you scared?
        - Yes
        - No
    - Do you feel unsafe?
        - Yes
        - No

Each of these are simple 3 or 4 word questions, each of which are parallel. In the case of “No” the next question should get asked. In the case of “Yes” the tool should try to narrow the broad issue down to something more concrete and then finally into an instruction that the user can follow or an action that the can be done for the user.

For “are you lost” the “yes” code could look at the current location and see if it is close enough to a number of “safe” locations and help the user get to those locations. If the user isn’t close enough, it could integrate with a transportation service to get the user taken to a safe location. If the user doesn’t have “safe” locations, the app could contact a helper or could contact the police.

The current code uses a non-structured tree.
For any given question, there are a series of responses. The responses include a text label and an action to take. At present, the actions include: “ask more questions” and “end case”. Clearly this needs more, such as a way to delegate to other activities if need be.

A particular question page contains:

    - Name - a non-translated unique name
    - Text - text to display for the question
    - Responses - a list of responses to present to the question

A response contains:

    - Text - text to display on a button
    - UniqueID - a non-translatable tag which is unique for this response in its set. Maybe not needed. Can use Text?
    - ResponseAction - the type of action to take
    - ResponseParameter - a parameter that defines how the action should be done.

For the “ask more questions” response, the parameter is a string which is the **Name** of the next question to ask. For “end case” responses, this will text to display to the user for the end case.

Hierarchy exists where it is created and is not mandated. As such, this could be represented in XML like this:

    <helpme version="1.0" lastUpdate="date-time">
      <question name="Hurt" Text="Are you hurt?" >
        <responses>
          <response Text="Yes" UniqueID="Yes" ResponseAction="FurtherQuestion" ResponseParameter= "Bleeding" />
          <response Text="No" UniqueID="No" ResponseAction="FurtherQuestion" ResponseParameter="Lost" />
        </responses>
      </question>
      <question name="Lost" Text="Are you lost?" >
        <responses>
          <response Text="Yes" UniqueID="Yes" ResponseAction="FurtherQuestion" ResponseParameter="SafeLocation" />
          <response Text="No" UniqueID="No" ResponseAction="EndCase" ResponseParameter="I'm sorry, I don't know how to help you." />
        </responses>
      </question>
    </helpme>

There’s no particularly good reason why this is not an explicit tree except that might make it easier to build/edit using canned top-level questions.

It’s reasonable for this data to be stored within the app. Editing will be interesting. There could be a way to retrieve the tree at app startup as well from a remote location and to download it and use it if it is newer and compatible.

