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
VAR failure = true
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Jasper
Someone's left their coin pouch here. We should take it. The money could be useful on our travels - do you know how much armor costs these days?
+[Continue. #EdrickContinue]
~IncreaseTension(2)
#Yael
Absolutely not. Taking something that belongs to someone else is <color=red>wrong</color>. Fate-Struck don't steal.
++[Continue. #EdrickContinue]
#Jasper
Big talk for someone who stole my hotcakes.
+++[Contine #EdrickContinue]
#YaelPensive
...That's a matter of perspective. Leave the coin pouch, at once.
++++[Continue #EdrickContinue]
#Jasper
No way, we're taking it.
+++++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should keep the coin pouch. #EdrickChoice]
~SetJaspersWay()
->JASPER
+[We should leave the coin pouch. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[You two decide. #EdrickChoice]
~CalculateEventResults()
->EDRICK
->DONE

=YAEL
{yaelsWay: 
#Yael
{tension > 13: You heard the Calibrator, leave it. | Doing the right thing is always rewarded. You'll see. }
}
+[Continue #EdrickChoice]
#Jasper
{tension > 13: What am I, a dog? Gods. | Well, don't complain when we get hungry. }

++[Okay, let's try this. #EdrickChoice]
~CalculateEventResults()
->YAELCOMP

=JASPER

{jaspersWay: 
#Jasper
{tension > 13: At least Edrick has some sense in him. I'm going to buy a new shield. | I'm sorry, Yael, but desperate times call for desperate measures.}
}

+[Continue. #EdrickChoice]
#YaelPensive
{tension > 13: You'd be buying it with stolen money. I hope each side of your bedroll is too hot or too cold. Whichever one you don't like. | ...I don't agree, but you're free to do what you would like.}
++[... #EdrickChoice]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END

=EDRICK
#Jasper
{tension > 10: Alright. No questions here. I'm taking it. | ...alright. Let's just drop the subject and move on.}
~IncreaseTension(3)
->DONE

=JASPERCOMP
#Jasper
{success: {tension > 6: What did I ever do to you to warrant such a curse? Are you going to keep insulting me when I buy all your food tomorrow? | Damn right I am. You don't have to spend any of this.} |  {tension > 6: Eh, forget it. We've got more important things to do. | ...fine, I guess we should respect whoever dropped this. Doesn't mean you're right.  }}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
->DONE

=YAELCOMP
{passive: You did nothing.}
{success: {tension > 6: I have half a mind to march you back over to the village so you can return it yourself, Jasper. | I'll hang the pouch up on this post. I'm sure the owner will come back to look for it...} | {tension > 6: See? Edrick agrees. I think we can all agree that stealing is wrong. | I don't even care anymore, to be honest. }}
#Yael
+[Continue #EdrickChoice]
#Jasper
{success: {tension > 6: You talk like you've never been robbed before. | Alright, whatever, you win. Let's move on. } | {tension > 6: Okay, whatever you say, Princess. I'm taking the bag. | Forget it. Let's go.}}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
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