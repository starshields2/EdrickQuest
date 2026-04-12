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
+[.... #EdrickContinue]
~IncreaseTension(1)
#Jasper
We can chop a tree down over the ravine and walk across.
++[... #EdrickContinue]
That's too risky! Do you forget your <color=red> missing finger?</color> I'll use one of my scrolls. We can easily use magic to fix the bridge.
~ChangeNotesIndex(1)
~UpdateNote()
#YaelAngry
+++[... #EdrickContinue]
#Jasper
This time will be different. I'll cut that tree down. 
++++[Right. #EdrickContinue]
Edrick, don't be ridiculous. I will fix that bridge. 
#YaelPensive
+++++[I don't doubt either of your capabilities. #EdrickChoice]
->JASPERSTELL
=JASPERSTELL
#JasperTell
And waste precious scrolls? Don't be stupid. I'm knocking a tree down. 
*[Jasper, what's wrong? #EdrickContinue]
    {YaelTell == 1: You don't have to coddle me like a child, Edrick. I'm <i>fine.</i> It's Yael wasting all our scrolls who needs your help!} 
    {YaelTell == 0: Don't worry about it. Let's chop this tree down.} 
    {YaelTell == 2: I've never used magic for something like this. What if it goes wrong, and we <i>die</i>. Who will save the Valley then?!}
    
++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should use magic. #EdrickChoice]
~IncreaseTension(13)
~SetYaelsWay()
->YAEL
+[We should use the tree. #EdrickChoice]
~SetJaspersWay()
->JASPER
->EDRICK
->DONE

=YAEL
{yaelsWay: 
#Yael
{tension > 13: Good. Now let me perform this ritual, <i>with no distractions.</i> That means no complaining. | I just wouldn't have liked it if any of us got squashed. }
}
+[Continue #EdrickChoice]

    {YaelTell == 2: Edrick! I just told you how scared I was to use magic, and you still go with <i>her</i> idea?!}
    {tension > 13: Don't give me anything else to complain about. Or I'll start complaining. | I wouldn't have wanted to fall to my death either, but alright! }   
#Jasper
++{YaelTell != 2} [Okay, let's move this along. #EdrickChoice]
~CalculateEventResults()
->YAELCOMP
++{YaelTell == 2} [I'm sorry, but it's the right way to go. #EdrickChoice]
~IncreaseTension(6)
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
{success: {tension > 10: And that's where hard work will get you! Were you going to read our way across the gap? | See. No accidents. I knew I could do it.} |  {tension > 10: ...you still have those scrolls, right?  Just one more chop will do it. I swear. One more.   }}
{success: {tension > 10: {DecreaseTension(3)}|{DecreaseTension(5)} }| {tension > 10: {IncreaseTension(5)} | {IncreaseTension(3)}}}
+[What an interesting outcome. #EdrickContinue]
{success: {tension > 10: You could have lost another finger, Jasper! No need for such bravado. | I was worried about you.} |  {tension > 10:  ...Hm. I thought we weren't supposed to be <i>wasting</i> them.|  You've been swinging at it for ages now, just let it go. We're wasting time. }}
#Yael
++{success} {tension >= 10} [I think we all deserve a break. #EdrickChoice]
->DONE
++{!success} {tension < 10} [Looks like we're going beneath the bridge after all. #EdrickChoice]
->DONE
++{success} {tension < 10} [Good work. Let's get moving. #EdrickChoice]
->DONE
++{!success} {tension >= 10} [Take a few deep breaths. #EdrickChoice]
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