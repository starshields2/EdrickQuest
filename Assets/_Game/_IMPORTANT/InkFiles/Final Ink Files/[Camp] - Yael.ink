//InkOnly

VAR jaspersWay = false
VAR yaelsWay = false


//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 0
VAR NotesIndex = 0
VAR success = false
VAR failure = true
VAR passive = false
VAR type = 3
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = =BEGINNING
->TALK
=TALK
You had something to say? #Yael
+[Ask about Jasper #EdrickChoice] ->JASPER

->DONE
+[Ask about the Ritual #EdrickChoice] ->START
+[Ask about herself #EdrickChoice] ->HERSELF
#Edrick
What do you want to do?
#Yael
->DONE
=JASPER
#Yael
What about her?
+[How do you feel about your Tether? #EdrickChoice]
#Yael
There's not much I can do about that, is there? We are Tethered already. As long as she keeps her mess on her side of the camp, I'm perfectly fine.
->DONE
+[Have you ever traveled with anyone like her before? #EdrickChoice]
->DONE
=START
#Yael
Oh yes! I have everything ready. The needle, thread and basin, I trust are with your things?
->DONE
=HERSELF
#Yael
Me?
+[What do you do in your spare time? #EdrickChoice]
My routine consists of a strict regimen. Prayer, then I fold my extra clothes, and take inventory of our resources. All very exciting, isn't it?
->DONE
+[Have you really read all the books in your tent? #EdrickChoice]
#Yael
Of course. My favorite is the <i>Starlight Mansion</i>. A work of fiction by the finest author in the city. I bought it on release.
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


EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()