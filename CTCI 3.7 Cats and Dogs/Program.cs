using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_3._7_Cats_and_Dogs
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(3, 7, "Cats and Dogs");

            TwoQueues();

            OneQueue();            

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// Create a new class descending from Queue<Animal>
        /// Create methods for DequeueCat, DequeueDog, DequeueAny, etc, which
        /// hide the default Queue<> methods.
        /// 
        /// </summary>
        private static void OneQueue()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Solution with a single queue:");
            Console.WriteLine();

            AnimalQueue AnimalQueue = new AnimalQueue();

            for (int i = 0; i < 7; ++i)
            {
                AnimalQueue.Enqueue(new Cat());
                AnimalQueue.Enqueue(new Dog());
            }

            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 animals!");
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();
            
            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 cats!");
            AnimalQueue.DequeueCat();
            AnimalQueue.DequeueCat();
            AnimalQueue.DequeueCat();
            
            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 dogs!");
            AnimalQueue.DequeueDog();
            AnimalQueue.DequeueDog();
            AnimalQueue.DequeueDog();
            
            Console.WriteLine();
            Console.WriteLine("The rest of the animals are escaping!");
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();
            AnimalQueue.Dequeue();            
        }

        /// <summary>
        /// 
        /// Create a class containing 2 queues, 1 for cats and 1 for dogs
        /// 
        /// </summary>
        private static void TwoQueues()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Solution with two queues:");
            Console.WriteLine();

            CatsAndDogs shelter = new CatsAndDogs();
            for (int i = 0; i < 7; ++i)
            {
                shelter.Enqueue(new Cat());
                shelter.Enqueue(new Dog());
            }

            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 animals!");
            shelter.DequeueAny();
            shelter.DequeueAny();
            shelter.DequeueAny();


            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 cats!");
            shelter.DequeueCat();
            shelter.DequeueCat();
            shelter.DequeueCat();


            Console.WriteLine();
            Console.WriteLine("Someone is adopting 3 dogs!");
            shelter.DequeueDog();
            shelter.DequeueDog();
            shelter.DequeueDog();


            Console.WriteLine();
            Console.WriteLine("The rest of the animals are escaping!");
            shelter.DequeueAny();
            shelter.DequeueAny();
            shelter.DequeueAny();
            shelter.DequeueAny();
            shelter.DequeueAny();
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    class CatsAndDogs
    {
        Queue<Cat> CatQueue = new Queue<Cat>();
        Queue<Dog> DogQueue = new Queue<Dog>();        

        public void Enqueue(Animal passed_animal)
        {
            System.Threading.Thread.Sleep(10); // necessary to prevent ties when determining the oldest animal
            

            if (passed_animal is Cat)
            {
                Console.WriteLine("The shelter welcomes " + passed_animal.name + " the cat.");
                CatQueue.Enqueue(passed_animal as Cat);
            }
            else
            {
                Console.WriteLine("The shelter welcomes " + passed_animal.name + " the dog.");
                DogQueue.Enqueue(passed_animal as Dog);
            }
        }

        public void DequeueAny()
        {
            if ((CatQueue.Count > 0) && (DogQueue.Count > 0) && (CatQueue.Peek().Arrival < DogQueue.Peek().Arrival))
            {
                DequeueCat();
            }
            else if ((DogQueue.Count > 0) && (CatQueue.Count > 0)) // dogs are older than cats
            {
                DequeueDog();
            }
            else if (CatQueue.Count > 0)  // there are cats but not dogs
            {
                DequeueCat();
            }
            else if (DogQueue.Count > 0) // there are dogs but no cats
            {
                DequeueDog();
            }
            else // there are neither cats nor dogs
            {
                Console.WriteLine("Someone tried to adopt an animal, but the shelter is empty.");
            }
        }

        public void DequeueCat()
        {
            if (CatQueue.Count > 0)
            {
                Console.WriteLine("The shelter bids farewell to " + CatQueue.Peek().name + " the cat.");
                Cat thisCat = CatQueue.Dequeue();
            }
            else
            {
                Console.WriteLine("Someone tried to adopt a cat, but there are no more cats in the shelter.");
            }
        }

        public void DequeueDog()
        {
            if (DogQueue.Count > 0)
            {
                Console.WriteLine("The shelter bids farewell to " + DogQueue.Peek().name + " the dog.");
                Dog thisDog = DogQueue.Dequeue();
            }
            else
            {
                Console.WriteLine("Someone tried to adopt a dog, but there are no more dogs in the shelter.");
            }
        }
    }

    class Animal
    {
        static int count = 0;

        public DateTime Arrival;
        public string name;

        public Animal()
        {
            Arrival = DateTime.Now;

            name = GetName();
        }

        private string GetName()
        {
            if (count > 9)
                count = 0;

            switch (++count)
            {
                case 1:
                    return "Fifi";
                case 2:
                    return "Max";
                case 3:
                    return "Rusty";
                case 4:
                    return "Mr. Splashy";
                case 5:
                    return "Pricess Caroline";
                case 6:
                    return "Snuggles";
                case 7:
                    return "Rudolph";
                case 8:
                    return "Kermit";
                default:
                    return "Casper";                    
            }

            
        }
    }

    class Cat : Animal
    {
    }

    class Dog : Animal
    {

    }

    class AnimalQueue : Queue<Animal>
    {
        public void Enqueue(Animal passed_animal)
        {
            if (passed_animal is Cat)
                Console.WriteLine("The shelter welcomes " + passed_animal.name + " the cat.");
            else
                Console.WriteLine("The shelter welcomes " + passed_animal.name + " the dog.");

            base.Enqueue(passed_animal);
        }

        public Animal Dequeue()
        {
            if ((Count > 0) && (Peek() is Cat))
                return DequeueCat();
            else if (Count > 0)
                return DequeueDog();
            else
                Console.WriteLine("Someone tried to adopt an animal, but the shelter is empty.");
            return null;
        }

        public Animal DequeueCat()
        {
            if ((Count > 0) && (Peek() is Cat))
            {
                Console.WriteLine("Someone adopted " + Peek().name + " the cat.");
                return base.Dequeue();
            }

            Queue<Animal> tempQueue = new Queue<Animal>();

            while ((Count > 0) && (base.Peek() is Dog))
            {
                tempQueue.Enqueue(base.Dequeue());
            }

            if (Count == 0)
            {
                while (tempQueue.Count > 0)
                    base.Enqueue(tempQueue.Dequeue());
                Console.WriteLine("Someone tried to adopt a cat, but there are no cats left in the shelter.");
                return null;
            }

            Animal thisCat = base.Dequeue();

            while (Count > 0)
                tempQueue.Enqueue(base.Dequeue());

            while (tempQueue.Count > 0)
                base.Enqueue(tempQueue.Dequeue());
            
            Console.WriteLine("Someone adopted " + thisCat.name + " the cat.");
            return thisCat;
        }

        public Animal DequeueDog()
        {
            if ((Count > 0) && (Peek() is Dog))
            {
                Console.WriteLine("Someone adopted " + Peek().name + " the dog.");
                return base.Dequeue();
            }

            Queue<Animal> tempQueue = new Queue<Animal>();

            while ((Count > 0) && (Peek() is Cat))
            {
                tempQueue.Enqueue(base.Dequeue());
            }

            if (Count == 0)
            {
                while (tempQueue.Count > 0)
                    base.Enqueue(tempQueue.Dequeue());
                Console.WriteLine("Someone tried to adopt a dog, but there are no dogs left in the shelter.");
                return null;
            }

            Animal thisDog = Dequeue();

            while (Count > 0)
                tempQueue.Enqueue(base.Dequeue());

            while (tempQueue.Count > 0)
                base.Enqueue(tempQueue.Dequeue());

            Console.WriteLine("Someone adopted " + thisDog.name + " the dog.");
            return thisDog;
        }
    }
}
