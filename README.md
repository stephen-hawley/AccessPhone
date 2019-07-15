# AccessPhone

My daughter has physical and cognitive issues that make it near impossible for her to use
any phone that is available on the market. This project is an attempt to solve the big
picture question "how can I make a phone useable and useful to my daughter?"

There are two types of phone available on the market today: flip phones and smart phones.
Both of those present issues. Since the deficiencies in smart phones can be corrected via
software, this is the obvious target that we should take.

The goal is to create an App that makes is super easy for a person with cognitive
limitations to operate a phone in a meaningful way to help with their lives.

The app is required to take over the phone so that it be existed without assistance from
a supervisor. The app will then act as a hub to assist in these tasks:
- communication with assitance
- payment and budgeting
- appointments and time managment
- transportation
- operate NFC devices (locks)
- take and share pictures
- emergency services

Each of these activities should be configurable such that they can be made available or
unavailable conditionally or based on the time of day.

Where possible, the activities should leverage existing phone infrastructure but should
hide the complexity.

For example, for a person who has speech that can't readily be understood by speech
recognition, the app should have an option to record speech and send it as an attachment
via SMS or email (depending on what is available). This would not involve implementing
an email client and contact service, but instead use the platform ones in a headless
way.

There should be options to allow a supervisor to either check or be informed of usage
of the app. For example, the supervisor might want to be informed when the user attempts
to exceed their budget.
