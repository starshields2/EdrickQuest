//InkOnly
VAR YaelTell = 0
VAR jaspersWay = false
VAR yaelsWay = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
//VAR failure = false
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene


Ah, stop, stop. This creature is in pain. Looks like a downed bird. Poor thing's wings are broken.
+[Alright, we're stopping. #EdrickContinue]

#Jasper
So unfortunate... I'll put it out of its misery. Turn around, Yael, don't look.
++[Continue. #EdrickContinue]
#YaelAngry
No! I can heal it. I regard <b>all</b> life with dignity, and I can prolong this one!
+++[Contine #EdrickContinue]
#Jasper
And waste precious time? Don't be silly. I've seen magic like yours. A healing spell will drain your strength.
++++[We have to make a decision. #EdrickContinue]
->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should they do?
+[Yael should heal it. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[Jasper should kill it. #EdrickChoice]
~SetJaspersWay()
->JASPER
+[You two decide. #EdrickChoice] 
->EDRICK
->DONE

=YAEL
#Yael
{tension > 13: You've made no mistake in listening to my wisdom. This bird will live! | Let's see if this will work. }
#Yael
+[Let's do this. #EdrickContinue]
~CalculateEventResults()
->YAELCOMP
->DONE

=JASPER
#Jasper

{tension > 13: City folk make me do everything. I'll get my hands dirty this time. Look away, Princess. | Just turn around, Yael. Sorry it had to be this way. }

+[Okay, let's try this. #EdrickContinue]
#Yael
{tension > 13: Do whatever you want, but the stars are watching. | Gods. Should I cover my ears, too? }
++[Do what you have to do. #EdrickChoice]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END
=EDRICK
#Yael
I can't let the life of a creature go to waste, Edrick. I don't care what Jasper thinks, I'm healing it. 
+[... #EdrickChoice]
~IncreaseTension(3)
#Jasper
You're going to waste time, and scrolls! Get that through your head! Our mission is more important than some bird!
++[The Tether will snap if you keep arguing. #EdrickChoice]
#Yael
Fine.
~IncreaseTension(3)
#Jasper
Let it!
~IncreaseTension(3)
+++[... oh, dear. #EdrickChoice]
~CalculateEventResults()
->DONE

=JASPERCOMP
#Jasper
{success: {tension > 12: There. It's been done. Now let's get going so I can find somewhere to wash up. | Sorry you had to see that. Maybe we can bury it together.} |  {tension > 12: I can't do it. I can't do it. I just can't do it. Let's just go. Poor thing. | ...You know. Maybe we shouldn't disturb the thing. Not that I'm chickening out or anything.  }}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
->DONE

=YAELCOMP
#Yael
//pass: everyone is happy
//fail: yael gets tired
{passive: You did nothing.}
{success: {tension > 9: See Jasper, I've mended its wound. Imagine if it had died! It would have been on our consciences forever. | There. Now it can fly back home...} | {tension > 9: There, see. Alive and well. Doing the right thing makes one <i>tired</i>, so I'm going to need to take a break. | I need to rest. Can we take a break? }}
+[Continue #EdrickChoice]
#Jasper
{success: {tension > 6: The <i>imminent darkness</i> would have been on my conscience too, you know. Gods. | At least its alive. Now no one has to do the dirty work. } | {tension > 6: I wish I could roll my eyes into my head like a pair of marbles. | Yeah yeah, take all the time you need...}}
{success: {tension > 10: {DecreaseTension(5)}|{DecreaseTension(3)} }| {tension > 10: {IncreaseTension(2)} | {IncreaseTension(2)}}}
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

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount

EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()