import React from "react";
import {
  Document,
  Page,
  Text,
  View,
  StyleSheet,
  Image,
} from "@react-pdf/renderer";
// Create styles

const Bill = (props) => {
  const { reservation } = props;

  const QRCodeView = (id) => {
    const dataUrl = document
      .getElementById(`reservation${reservation.reservationId}`)
      .toDataURL();
    return (
      <View style={{ margin: 10, padding: 10 }}>
        <Image
          allowDangerousPaths
          src={dataUrl}
          style={{ width: "200px", marginHorizontal: "auto" }}
        />
      </View>
    );
  };
  return (
    <Document>
      <Page size="A4">
        <View style={{ margin: 10, padding: 10 }}>
          <Text
            style={{
              textAlign: "center",
            }}
          >
            FTI Clinic
          </Text>
        </View>
        <View style={{ margin: 10, padding: 10 }}>
          <Text>Emri: {reservation.firstName}</Text>
          <Text>Mbiemri: {reservation.lastName}</Text>
          <Text>Gjinia: {reservation.gender}</Text>
          <Text>Nr Identitetit: {reservation.identityNumber}</Text>
          <Text>Datelindja: {reservation.birthday}</Text>
        </View>
        <View
          style={{
            display: "flex",
            justifyContent: "center",
            margin: 10,
            padding: 10,
          }}
        >
          <Text style={{ fontStyle: "italic" }}>
            Ju lutem ruani faturen nese deshironi te keni te drejten per ta
            anulluar
          </Text>
        </View>
        <QRCodeView id={reservation.reservationId} />
      </Page>
    </Document>
  );
};

export default Bill;
