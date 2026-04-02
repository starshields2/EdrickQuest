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


Ah, stop, stop. This creature is in pain. Looks like a downed bird.
+[Alright, we're stopping. #EdrickContinue]

#Jasper
So unfortunate... I'll put it out of its misery. Turn around, Yael, don't look.
++[Continue. #EdrickContinue]
#YaelAngry
No! I can heal it. I regard <b>all</b> life with dignity, and I can prolong this one!
+++[Contine #EdrickContinue]
#Jasper
And waste precious time? Don't be silly.
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
+[You two decide. #EdrickChoice] ->EDRICK
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
{tension > 10: I can't watch the life of a creature go to waste. | ...alright. Let's just drop the subject and move on.}
~IncreaseTension(3)
+[... #EdrickChoice]
#Jasper
Alright, let's go.
->DONE

=JASPERCOMP
#Jasper
{success: {tension > 12: There. It's been done. Now let's get going so I can find somewhere to wash up. | Sorry you had to see that. Maybe we can bury it together.} |  {tension > 12: I can't do it. I can't do it. I just can't do it. Let's just go. Poor thing. | ...You know. Maybe we shouldn't disturb the thing. Not that I'm afraid.  }}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
->DONE

=YAELCOMP
#Yael
{passive: You did nothing.}
{success: {tension > 6: See Jasper, I've mended its wound. Imagine if it had died! It would have been on our consciences forever. | There. Now it can fly back home...} | {tension > 6: Even my healing magic wasn't enough for this little one. Maybe it would have been if we hadn't spent so much time bickering. | I did what I could. Now we bury it. }}
+[Continue #EdrickChoice]
#Jasper
{success: {tension > 6: The <i>imminent darkness</i> would have been on my conscience too, you know. Gods. | At least its alive. Now no one has to do the dirty work. } | {tension > 6: It also wouldn't be in pain if we just put it out of its misery like I said. | Now we bury it. And we move forward.}}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
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