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
What do you mean, you didn't bring one? I made such a fuss earlier about how <color = green> prepared </color> we were for this! I was starting to think you were capable! 
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
What should I do?
+[Talk to Yael. #EdrickChoice]
->YAEL
+[Talk to Jasper. #EdrickChoice]
->JASPER
+[I think I know what's wrong. #EdrickChoice]
->RESOLUTION

=YAEL
#Yael
Jasper shouuld have brought her compass. It's insane to be so ill prepared for a journey like this. 
+[I agree. #EdrickChoice]

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
I don't know why Yael's so mad. 
+[I think she really values being prepared.]
->JASPERCOMP
->DONE

->END
=RESOLUTION

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