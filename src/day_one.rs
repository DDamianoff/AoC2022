use std::fs;

const DAY: &str = "one";

fn main() {
    let input = fs::read_to_string(format!("./src/inputs/day_{DAY}.txt"))
        .expect("Not hable to read that file tho");

    let separated_input = input.split("\n\n");

    let mut elves : Vec<i32> = vec![0];

    for string_elf in separated_input {
        let mut total = 0;
        let value_list = string_elf.split("\n");

        for value in value_list {
            let as_int : i32 = value.parse().unwrap();
            total += as_int;
        }

        elves.push(total);
    }

    elves.sort();
    elves.reverse();

    println!("Top Tier Elf: {:?}", &elves[0]);

    println!("Top Tier Elves: {:?}", &elves[0..3]);

    let mut final_result = 0;

    for cal in &elves[0..3] {
        final_result += cal;
    }

    println!("Total calories of Top Tier Elves: {final_result}");
}
