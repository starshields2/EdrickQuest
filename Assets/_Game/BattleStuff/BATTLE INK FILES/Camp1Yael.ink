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
VAR type = 3
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

=BEGINNING
You had something to say? #Yael
~IncreaseTension(1)
+[Talk about the Ritual #EdrickChoice] -> START
+[Talk about Jasper #EdrickChoice] -> JASPER
+[Ask her about herself #EdrickChoice] ->HERSELF
+[Nevermind.] ->DONE
->DONE
=JASPER
#Yael
What about her?
*[Are you and Jasper getting along well? #EdrickChoice]
I believe that the Ritual is more important than whether or not we get along. I have my reservations about her, of course. I'm not sure why the Sunblades don't visit the city more often.
++[. #EdrickChoice] ->JASPER
*[Rock #EdrickChoice]

What am I supposed to do with this?
++[I don't know. #EdrickChoice] ->JASPER
+[Nevermind. #EdrickChoice] ->START

->DONE
=START
#Yael
Oh yes! I have everything ready. The needle, thread and basin, I trust are with your things?
*[Yes, I have them. #EdrickContinue]
#Yael
Do you take me for a fan of the apocalypse? I'd rather work with a country bumpkin than watch my people suffer.
***[Really? #EdrickContinue]
Yes, really. The equinox has no preference for sun or moon. It will destroy all, unless we destroy it.
****[Do you think we can do it? #EdrickContinue]
Of course I do. I know I can lead us to a favorable outcome, if Jasper listens to me.
*****[I believe in you. #EdrickContinue]

No need to believe in something that is factual. If you trust me, you simply know it. 
->DONE
*****[Are you sure?]

Why are you prodding at me like a horse? Time will tell. Now go to bed.
->DONE
***[Country bumpkin?] 
->DONE
**[How will you prepare for the journey ahead?]
#Yael
The Goddess will bless me and keep me, all I must do is give myself to her.
***[You seem rather devoted. #EdrickContinue]

I pride myself in that. I am to be the next Cresent-maiden after all - and a priestess that strays from the path is no priestess at all. 
->DONE
***[Give yourself to her? Doesn't that sound a bit... strange? #EdrickContinue]
#Yael
It is our way. Moonwalkers can't see themselves as people, only vessels for her will. You will do well to not insult my tradition.

****[Well, what's her will right now? #EdrickContinue]
To go to bed, obviously. 

->DONE

**[What will you do in your free time? #EdrickContinue] 
#Yael
Most likely I will return to prayer. I don't find anything here endearing enough to go outside. It's just gross. 
->DONE
=HERSELF
#YaelPensive
What would you like to know, Calibrator?
*[You mentioned you were wanting to be Tethered to someone else. #EdrickContinue]
#Yael
...It's true. My wife, Kethora, and I were hoping to be Tethered. But I suppose it was naive. There's no way to influence the Tethering, even if it seems to favor some people.
**[I noticed you #EdrickContinue]
->DONE
->END

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