package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"net/http"

	"golang.org/x/net/html/charset"
)

func main() {
	res, err := http.Get("http://rusbonds.ru/ank_obl.asp?tool=99686")
	if err != nil {
		log.Fatal(err)
	}
	utf8, err := charset.NewReader(res.Body, res.Header.Get("Content-Type"))
	if err != nil {
		fmt.Println("Encoding error:", err)
		return
	}

	body, err := ioutil.ReadAll(utf8)
	res.Body.Close()
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("%v", string(body))
}
