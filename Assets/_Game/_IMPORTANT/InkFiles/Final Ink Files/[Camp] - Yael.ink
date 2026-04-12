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
+[Ask about the Ritual #EdrickChoice] ->START
+[Ask about herself #EdrickChoice] ->HERSELF
+[Never mind. #EdrickChoice] ->DONE

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
++[That's true. #EdrickChoice]
->TALK
+[Have you ever traveled with anyone like her before? #EdrickChoice]
->TALK
=START
#Yael
Oh yes! I have everything ready. The sooner we can finish, the better.
+[What will you do when it's over? #EdrickChoice]
#Yael
I want to start a bakery. I think that would be nice. I may even travel.
++[Sounds delicious. #EdrickContinue]
->TALK
=HERSELF
#Yael
Oh, me? You're interested?
+[What do you do in your spare time? #EdrickChoice]
My routine consists of a strict regimen. Prayer, then I fold my extra clothes, and take inventory of our resources. All very exciting, isn't it?
++[Very organized. #EdrickChoice]
#Yael
Indeed. My father taught me everything, and one of the things he stressed most was that a stress free environment leads to a stress free life.
+++[I see... that's very true. #EdrickChoice]
->TALK
++[This is the one time to explore what's outside the city. Why not do that? #EdrickChoice]
#Yael
Why would I do that? It's dangerous out here. And I would have to bring Jasper. No thank you.
->TALK
+[Have you really read all the books in your tent? #EdrickChoice]
#Yael
Of course. My favorite is the <i>Starlight Mansion</i>. A work of fiction by the finest author in the city. I bought it on release, and the protective cloth cover is still there.
++[How long did it take you to read those? #EdrickChoice]
#YaelPensive
I'm a slow reader. The first book took me two years. 
+++[You should probably get reading, then. #EdrickChoice]
->TALK
+[The ring on your hand... #EdrickChoice]
#YaelPensive
Before I was chosen, I was engaged. A magistrate in the city. I knew there was no guarantee I would come back, so we called it off. But the rings stay on.
++[That's sweet. #EdrickChoice]

->TALK
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