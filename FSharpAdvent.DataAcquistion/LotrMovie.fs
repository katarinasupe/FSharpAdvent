module LotrMovie

open FSharp.Data

open System
open System.IO

type Book = 
    | TheFellowshipOfTheRing
    | TheTwoTowers
    | TheReturnOfTheKing

type LotrData = { BookName    : Book; 
                  ChapterName : string; 
                  ChapterData : string }

type CharacterMentions     = { CharacterName : string; CharacterMentions : int; }
type ChapterCharacterCount = { BookName : Book; Chapter : string; Count : int }

let charactersToGetMentionsFor = 
    [
        "Anborn"
        "Angbor"
        "Arod"
        "Arwen"
        "Asfaloth"
        "Angelica"
        "Dora" 
        "Frodo" 
        "Baranor"
        "Beechbone"
        "Beregond"
        "Bergil"
        "Bilbo" 
        "Bob"
        "Folco"
        "Fredegar" 
        "Bombadil"
        "Boromir"
        "Bruno"
        "Melilot"
        "Meriadoc"
        "Rorimac"
        "Bregalad"
        "Milo" 
        "Barliman" 

        "Celeborn"
        "Ceorl"
        "Bowman"
        "Carl"
        "Rosie"
        "Tolman"
        "Wilcome"
        "Círdan"

        "Damrod"
        "Denethor" 
        "Derufin"
        "Dervorin"
        "Duilin"
        "Duinhir"
        "Durin's Bane"
        "Dáin II Ironfoot"
        "Déagol"
        "Déorwine"
        "Dúnhere"

        "Elfhelm"
        "Elladan" 
        "Elrohir"
        "Elrond"
        "Éomer"
        "Éowyn"

        "Erestor"
        "Erkenbrand"

       "Faramir" 
       "Fastred" 
       "Bill Ferny"
       "Fimbrethil"
       "Finglas"
       "Firefoot"
       "Fladrif"
       "Forlong"

       "Galadriel"
       "Galdor"
       "Hamfast" 
       "Samwise"
       "Gamling"
       "Gandalf"
       "Ghân-buri-Ghân"
       "Gildor" 
       "Gimli"
       "Glorfindel"
       "Gléowine"
       "Glóin"
       "Golasgil"
       "Goldberry"
       "Gollum"
       "Gorbag"
       "Gothmog" 
       "Grimbeorn"
       "Grimbold"
       "Grishnákh"
       "Gwaihir"
       "Gárulf"

       "Halbarad"
       "Haldi"
       "Harding"
       "Hasufel"
       "Mat Heahertoes"
       "Herefar"
       "Herubra"
       "Hirluin"
       "Hob"
       "Hugo"
       "Hurin"
       "Háma"

       "Imrahil"
       "Ingold"
       "Ioreth"

       "Lagduf"
       "Landroval"
       "Legolas"
       "Lindir"
       "Lugdush"
       "Mablung the Ranger"
       "Farmer Maggot"
       "Mauhúr"
       "Meneldor"
       "Mouth of Sauron"
       "Muzgash"
       "Nob"
       "Old Man Willow"
       "Old Noakes"
       "Orophin"
       "Otho Sackville-Baggins"
       "Mrs. Proudfoot"
       "Odo Proudfoot"
       "Sancho Proudfoot"
       "Radagast"
       "Radbug"
       "Rúmil"
       "Lobelia Sackville-Baggins"
       "Lotho Sackville-Baggins"
       "Ted Sandyman"
       "Saruman"
       "Shagrat"
       "Shelob"
       "Robin Smallburrow"
       "Snaga"
       "Snaga (Two Towers orc)"
       "Targon"
       "The King of the Dead"
       "Thranduil"
       "Théoden"
       "Théodred"
       "Adelard Took"
       "Everard Took"
       "Pippin"
       "Treebeard"
       "Daddy Twofoot"
       "Ufthak"
       "Uglúk"
       "Watcher in the Water"
       "Will Whitfoot"
       "Widow Rumble"
       "Willie Banks"
       "Windfola"
       "Witch-king of Angmar"
       "Gríma Wormtongue"
       "Wídfara"
       "Éothain"

    ]

[<Literal>]
let file = @"/Users/mukundraghavsharma/Desktop/F#/FSharpAdvent/Data/LotrBook.txt"

let data = File.ReadAllText( file )
let split = data.Split('<')
let filtered = 
    split
    |> Array.filter( fun r -> r <> "" )

let getBookName ( input : string ) : Book = 
    match input with
    | " The Return Of The King "     -> TheReturnOfTheKing
    | " The Two Towers "             -> TheTwoTowers
    | " The Fellowship Of The Ring " -> TheFellowshipOfTheRing
    | _ -> failwith "Book not found!"

let allChapterData = 
    filtered
    |> Array.map( fun f -> 
        let split1 = f.Split('>')
        let split2 = split1.[ 0 ].Split('~')
        { BookName = getBookName( split2.[ 0 ] ); ChapterName = split2.[ 1 ]; ChapterData = split1.[ 1 ].Trim() })

let fellowshipOfTheRing =
    allChapterData
    |> Array.filter( fun a -> a.BookName = TheFellowshipOfTheRing )

let twoTowers =
    allChapterData
    |> Array.filter( fun a -> a.BookName = TheTwoTowers )

let returnOfTheKing =
    allChapterData
    |> Array.filter( fun a -> a.BookName = TheReturnOfTheKing )

let splitValues = [| ' '; '.'; '-'; ','; '!' |]

let splitWords( data : LotrData[] ) : string [] =
    let splitAndCleaned = 
        data 
        |> Array.collect( fun c -> 
            c.ChapterData.Trim().Split( splitValues ))
        |> Array.map( fun c -> c.Replace("`", "")
                                .Replace("'", "")
                                .Replace("-", "")
                                .Replace(";", "")
                                .Replace("?", "")
                                .Replace("'", ""))
        |> Array.filter( fun c -> c <> "'" && c <> "" && c <> "-" && c.Length <> 0 )
    File.AppendAllLines( __SOURCE_DIRECTORY__ + "/words.txt", splitAndCleaned)

    splitAndCleaned

(* Word Counts *)

let wordCount ( lines : string ) : int =
    let cleanedLines = 
        lines.Split( splitValues )
        |> Array.filter( fun c -> 
            c <> "'" && c <> "" && c <> "-" && c <> " " && c.Length <> 0 )

    //printfn "%A" cleanedLines
    File.AppendAllLines( __SOURCE_DIRECTORY__ + "/words.txt", cleanedLines )
    Array.length cleanedLines

let totalWordCount = 
    splitWords( allChapterData )
    |> Array.length

let forWordCount = 
    splitWords( fellowshipOfTheRing )
    |> Array.length

let ttWordCount = 
    splitWords( twoTowers )
    |> Array.length

let rokWordCount = 
    splitWords( returnOfTheKing )
    |> Array.length

(* Word Counts Per Chapter *)

let getChapterCounts ( book : LotrData[] ) : ChapterCharacterCount[] = 
    book
    |> Array.map( fun c -> 
    
    )


(* Unique Words *)

let uniqueWords ( data : LotrData[] ) = 
    splitWords( data )
    |> Array.distinct

let uniqueWordCount ( data : LotrData[] ) = 
    let uniqueWordsInData = uniqueWords( data )
    uniqueWords( data )
    |> Array.length

let allUniqueWords     = uniqueWords( allChapterData )
let allUniqueWordCount = uniqueWordCount( allChapterData )
let forUniqueWordCount = uniqueWordCount( fellowshipOfTheRing )
let ttUniqueCount      = uniqueWordCount( twoTowers ) 
let rokUniqueCount     = uniqueWordCount( returnOfTheKing )

(* Character Mentions *)

let characterMentions ( character : string ) ( book : LotrData[] )= 
    let characterMentions = 
        splitWords( book ) 
        |> Array.filter( fun c -> c = character )
        |> Array.length
    { CharacterName = character; CharacterMentions = characterMentions }

let characterMentionsAllBooks = 
    charactersToGetMentionsFor
    |> List.map ( fun c -> characterMentions c allChapterData )

let characterMentionsForFOR =
    charactersToGetMentionsFor
    |> List.map ( fun c -> characterMentions c fellowshipOfTheRing )

let characterMentionsForTT =
    charactersToGetMentionsFor
    |> List.map ( fun c -> characterMentions c twoTowers )

let characterMentionsForROK =
    charactersToGetMentionsFor
    |> List.map ( fun c -> characterMentions c returnOfTheKing )