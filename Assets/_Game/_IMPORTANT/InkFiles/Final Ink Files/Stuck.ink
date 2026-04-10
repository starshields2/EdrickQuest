//InkOnly
VAR YaelTell = 0
VAR jaspersWay = false
VAR yaelsWay = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 0
VAR NotesIndex = 0
VAR success = false
//VAR failure = false
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Jasper
It's going to take at least a couple days to get back on track at this rate.
+[So it seems. #EdrickContinue]
~IncreaseTension(1)
#Yael
I lost my compass on the way down. Can I see yours, Jasper? Maybe we can figure this out.
++[Good idea. #EdrickContinue]
I didn't bring one. Nature is my guide.
~ChangeNotesIndex(1)
~UpdateNote()
#Jasper
+++[... #EdrickContinue]
#YaelAngry
What do you mean, you didn't bring one? I made such a fuss earlier about how <color=green> prepared </color> we were for this! I was starting to think you were capable! 
++++[Hold on... #EdrickContinue]
I am capable! I don't need all your fancy tools from the city to find my way around. 
~ChangeNotesIndex(2)
~UpdateNote()
~IncreaseTension(3)
#Jasper
+++++[Okay, let's just relax... #EdrickChoice]
->JASPERSTELL
=JASPERSTELL
#YaelTell
How am I supposed to relax?! How are we supposed to know where we are? 
*[Yael, talk to me. #EdrickContinue]
    {YaelTell == 1: Edrick, we're <i>lost!</i> What else is there to talk about?!} 
    {YaelTell == 0: I think the problem is clear, we don't know where we are.} 
    {YaelTell == 2: Edrick, if we are not careful, we could die here. We don't know where we are. How would we get back? What about my family? }
    
++[Now just hold on, it'll be alright. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What can I say to relieve some pressure?
+[Talk to Yael. #EdrickChoice]
->YAEL
+[Talk to Jasper. #EdrickChoice]
->JASPER
+[You'll figure it out. #EdrickChoice]
->INCREASETHETENSION
+[Regroup]
->REGROUP

=REGROUP
+[Listen to me. Here's what's wrong. #EdrickContinue]
#Jasper
Is it that someone's being a big baby who can't tell east from west?
++[Continue #EdrickContinue]
#YaelAngry
That's not true! 
+++[No. It's this. #EdrickContinue]
->CORECHOOSER


=CORECHOOSER
+[You both want to go home, and getting lost is stressing you out. #EdrickChoice]
->RESOLUTION
+[Jasper should have been more prepared. #EdrickChoice]
~IncreaseTension(2)
#Jasper
What? Why?
++[Continue #EdrickContinue]
#Yael
~DecreaseTension(3)
Because the fate of the valley is at stake. You can't let your ego get in the way of being ready for whatever is coming next.
+++[Continue #EdrickContinue]
#Jasper
Okay? And you can't insult me at every turn just because you're right.
++++[I agree. #EdrickContinue]
#Yael
I'm sorry. You're right... I can be better. 
+++++[Good thing we're getting this sorted out. #EdrickContinue]
->RESOLUTION
+[You need to focus on the mission at hand. #EdrickChoice]
->DONE
=YAEL

Jasper should have brought her compass. It's insane to be so ill prepared for a journey like this.
    {YaelTell == 2: Honestly. It's like she doesn't even have anybody to go home to. }
#YaelPensive
+[We absolutely will get to the Temple and back. #EdrickChoice]
#YaelPensive
How can you be so sure?
++[Because you are both more than capable. #EdrickChoice]
~DecreaseTension(6)
#YaelPensive
...We have come this far, haven't we?
+++[Yes. And We will go further. Together. #EdrickContinue]
->MEDIATIONSTART
++[Because you must. #EdrickChoice]
#YaelPensive
You're not wrong, but you could have offered a bit more reassurance than that.
{YaelTell == 2: And it could have been easier if <i>someone</i> had brought their tools.}
~DecreaseTension(2)
+++[I understand. #EdrickChoice]
->MEDIATIONSTART

=JASPER
I don't know why Yael's so mad...
+[I think she really values being prepared. #EdrickChoice]
#Jasper
Yeah, I can see that. But it's not like it's her stuff. I can find my own way.
**[You can, but you are working together here. #EdrickChoice]
#Jasper
Well, it sure doesn't feel like it. 
+++[Why not? #EdrickChoice]
#Jasper
Every time we talk about something, it's always "Fate-Struck don't do this, heroes don't do that." I wish she'd just talk to me like a normal person.
++++[I'm making a note of this. #EdrickContinue]
->MEDIATIONSTART
**[This is unfamiliar territory. What if you get lost? #EdrickChoice]
#Jasper
I won't get lost. 
+++[Have you ever gotten lost before? #EdrickChoice]
#Jasper
Yes.
++++[Interesting. #EdrickChoice]
->MEDIATIONSTART
->DONE

->END
=RESOLUTION
#Yael
...I think we both just want to go home.
+[Continue. #EdrickChoice]
#Jasper
I agree. We're tired, and getting lost wasn't in the cards, of course. 
++[Continue. #EdrickChoice]
#YaelPensive
...do you miss your friends?
+++[Continue. #EdrickChoice]
#Jasper
I don't know, do you miss your wife?
++++[Continue. #EdrickChoice]
#YaelPensive
Yes.
{YaelTell == 2: Completely.}
+++++[Continue. #EdrickChoice]
#Jasper
Then let's finish this thing so we can go home.
->DONE

=INCREASETHETENSION
#Jasper
I'm figuring out that you're a mean person, Yael. You're just mean.
~IncreaseTension(3)
+[This was a mistake... #EdrickContinue]
#YaelAngry
I'm figuring out that you're a psychopath. You don't care about anything at all! Not ettiqute, not preparedness, nothing! 
++[Remember when we all agreed we want to be respected? #EdrickChoice]
#Jasper
I'm remembering a lot of things right now.
+++[Continue #EdrickContinue]
#YaelPensive
...I do think we could circle back to that. And besides that. I think...
++++[Yes? #EdrickChoice]
->RESOLUTION
++[Maybe we should take a break. #EdrickChoice]
#Jasper
I agree. I could use some rest. And some peace and quiet.
->DONE
=YAELCOMP

{passive: You did nothing.}
{success: {tension > 13: See how we don't have to destroy everything in our path in order to get across? | There. Now we can cross, safe and sound...} | {tension > 13: Well, I said no distractions, and look what happened. Now we wasted a scroll <i>and</i> there's  no bridge. | ...All is well, we will find another way around. }}
#YaelPensive
+[Continue #EdrickContinue]

{YaelTell == 2: I won't forget what you did, Edrick. That was cold. }
{success: {tension > 10: Yeah yeah, and while we're at it let's donate all our belongings and live in a tree and drink tea all day. | Let's just go. } | {tension > 10: I wasn't even talking. I was barely talking! What's your problem with me?!. | Fine. Let's go.}}

{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(2)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
#Jasper
->DONE

==function YaelTellFalse(amount)
~YaelTell = 1
~return YaelTell

==function YaelTellTrue(amount)
~YaelTell = 2

==function IncreaseTension(amount)
~tension = tension + amount

==function DecreaseTension(amount)
~tension = tension - amount

==function SetJaspersWay
~jaspersWay = true

==function SetYaelsWay
~yaelsWay = true

==function ChangeNotesIndex(amount)
~NotesIndex = amount

==function ExtendedTutorial(amount)
~ExternalTutorialNum = amount

==function UpdatePopupNumber(amount)
~PopupNum = amount

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount

EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()