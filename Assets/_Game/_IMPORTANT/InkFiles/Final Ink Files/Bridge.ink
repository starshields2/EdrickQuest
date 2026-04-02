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

#Yael
The path forward has been destroyed. Look. The bridge is broken.
+[Continue. #EdrickContinue]
~IncreaseTension(1)
#Jasper
We can chop a tree down over the ravine and walk across.
++[Continue. #EdrickContinue]
#YaelAngry
That's too risky! I'll use one of my scrolls. We can easily use magic to levitate over the gap.
+++[Contine #EdrickContinue]
#Jasper
And waste precious scrolls? Don't be stupid. I'm knocking a tree down. 
++++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should use magic. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[We should use the tree. #EdrickChoice]
~SetJaspersWay()
->JASPER
~CalculateEventResults()
->EDRICK
->DONE

=YAEL
{yaelsWay: 
#Yael
{tension > 13: Good. Now let me perform this ritual, <i>with no distractions</i> That means no complaining. | I just wouldn't have liked it if any of us got squashed. }
}
+[Continue #EdrickChoice]
#Jasper
{tension > 13: Then don't give me anything to complain about. Or I'll start complaining. | I wouldn't have wanted to fall to my death either, but alright. }

++[Okay, let's move this along. #EdrickChoice]
~CalculateEventResults()
->YAELCOMP

=JASPER

{jaspersWay: 
#Jasper
{tension > 13: Step back, time for some good old fashioned elbow grease. And watch out for twigs, too. I don't know if you'd be able to lift them up with your noodle arms. | Well, make some distance so we can get going.}
}

+[Continue. #EdrickChoice]
#YaelPensive
{tension > 13: I was trained in ritual combat, Jasper. I can lift a twig. At least a dozen. | ...This had better work, Jasper.}
++[... #EdrickChoice]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END
=EDRICK
{success: We are going around the bridge. No arguments.| We will have to find another way forward. }
->DONE

=JASPERCOMP
#Jasper
{success: {tension > 10: And that's where hard work will get you! Were you going to read our way across the gap? | There. Nothing like good woodworking to calm the nerves.} |  {tension > 10: I guess I just don't know my own strength. Better than falling from that height though, right? | ...you still have those scrolls, right. No? Okay.  }}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
->DONE
->DONE
=YAELCOMP
#Yael
{passive: You did nothing.}
{success: {tension > 13: See how we don't have to destroy everything in our path in order to get across? | There. Now we can cross, safe and sound...} | {tension > 13: Well, I said no distractions, and look what happened. | ...All is well, we will find another way around. }}
+[Continue #EdrickChoice]
#Jasper
{success: {tension > 10: Yeah yeah, and while we're at it let's donate all our belongings and live in a tree and drink tea all day. | I guess it worked out just fine.. } | {tension > 10: I wasn't even talking. I was barely talking! What's your problem with me?!. | Fine. Let's go.}}

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