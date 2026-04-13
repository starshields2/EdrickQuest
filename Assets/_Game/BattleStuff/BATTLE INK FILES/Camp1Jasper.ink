//InkOnly

VAR jaspersWay = false
VAR yaelsWay = false


//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
//VAR failure = false
VAR passive = false
VAR type = 3
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene
#Jasper
What's on your mind?
->TALK
=TALK
+[Ask about Yael #EdrickChoice] ->YAEL
+[Ask about the Ritual #EdrickChoice] ->START
+[Ask about herself #EdrickChoice] ->HERSELF
+[Never mind. #EdrickChoice] ->DONE
->DONE

=YAEL
#Jasper
A bit of a stickler, isn't she. What do you want to know?
+[The Tether. Did you expect this to happen? #EdrickChoice]

Hell no. Most Sunblades are Tethered to a friend, or a sibling or something. And the achining from the Tether being established at such a long distance... I thought I was going to die.
#Jasper
++[It must have been scary. #EdrickChoice]
#Jasper
W-Well no, not scary. I'd never be scared.
+++[Of course. #EdrickContinue]
->TALK
+[Is there anything she can do to make working together easier? #EdrickChoice]
#Jasper
Oh, I don't think it's worth trying. She's off in her own little world most of the time.
++[You wouldn't know if you didn't try to talk candidly with her. #EdrickChoice]
#Jasper
Well. That's true. 
+++[So let's try, hm? #EdrickContinue]
->TALK
->DONE
=HERSELF
#Jasper
What do you wanna know?
+[What kind of things do you like? #EdrickChoice]
#Jasper
My favorite thing is a peach from the groves out south. Can't be beat. And you can make a killer cobbler with it too! 
++[I see you're quite the chef. #EdrickContinue]
->TALK
+[Have you ever been to a city? #EdrickChoice]
#Jasper
Nope, and don't plan on it. Streets too busy, and people too stuck up.
++[I see. #EdrickContinue]
->TALK
=START
Oh yeah, no big deal. Two more mountaintops to go.
+[Are you ready? #EdrickChoice]
#Jasper
Born ready. 
++[What will you do when it's over? #EdrickChoice]
#Jasper
I want to go home and try building a water mill. I think it's about time I settle down somewhere.
+++[I see. #EdrickContinue]
->TALK

->DONE
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




EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()